
using GarageApp.Interfaces;
using System.Text.RegularExpressions;

namespace GarageApp.Models;

internal class SearchParser : ISearchParser
{
    public string Instructions
    {
        get => $"[fritext (sträng)] [-t [fordonstyp (sträng)]] [-r [registreringsnummer (sträng)]] [-w [antal hjul (heltal). Lägg till '+' efter talet för att ange min-antal, och tvärtom med '-']] [-m [märke (sträng)] ]." +
            $"{Environment.NewLine + Environment.NewLine}Exempel:" +
            $"{Environment.NewLine}-t Car -w 4" +
            $"{Environment.NewLine}-r K7748" +
            $"{Environment.NewLine}k7748 -t Bike" +
            $"{Environment.NewLine}-w 3-";
    }

    /// <summary>
    /// Parse a search command string into a function that can be used in a Linq Where-method.
    /// 
    /// Splits the search command into two parts: one free text part that will match if the input
    /// text matches ANY property of the vehicle, and one part of flag-values, that will match if
    /// ALL pairs is matching. The final result is checking whether BOTH those groups are matching.
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    public Func<IVehicle, bool> Parse(string search)
    {
        List<Func<IVehicle, bool>> allPropsSearchFilters = new();
        List<Func<IVehicle, bool>> flagFilters = new();

        string pattern = @"^(?<search>[^\s]+)?(?<flags>(?:\s*-[a-z]\s+[^\s]+)*)$";

        var match = Regex.Match(search, pattern);

        string allPropsSearch = match.Groups["search"].Value;
        if (!string.IsNullOrEmpty(allPropsSearch))
        {
            foreach (char flagChar in new char[] { 't', 'r', 'w', 'm' })
            {
                string flag = $"-{flagChar} {allPropsSearch}";
                try
                {
                    allPropsSearchFilters.Add(GetFilterFunction(flag));
                }
                catch (Exception)
                {
                    // move on silently if any property doesn't like the input
                }
            }
        }

        var flags = Regex.Matches(match.Groups["flags"].Value, @"-[a-z]\s+[^\s]+")
                 .Select(m => m.Value);
        foreach (string flag in flags)
        {
            flagFilters.Add(GetFilterFunction(flag));
        }
        if (flags.Count() == 0)
        {
            return v => allPropsSearchFilters.Any(f => f(v));
        }
        return v => allPropsSearchFilters.Any(f => f(v)) && flagFilters.All(f => f(v));
    }

    private Func<IVehicle, bool> GetFilterFunction(string rawFlag)
    {
        (char flag, string value) = ParseFlag(rawFlag);
        switch (flag)
        {
            case 't': return v => string.Equals(v.GetType().Name.ToUpper(), value.ToUpper());
            case 'r': return v => v.RegistrationNumber.Contains(value.ToUpper());
            case 'm': return v => string.Equals(v.Manufacturer.ToUpper(), value.ToUpper());
            case 'w':
                return v =>
                {
                    var match = Regex.Match(value, @"(?<nr>\d+)(?<modifier>[+-])?");
                    if (!int.TryParse(match.Groups["nr"].Value, out int nr))
                    {
                        throw new FormatException($"Flagga '{rawFlag}' är felformulerad.");
                    }
                    if (string.Equals(match.Groups["modifier"].Value, "+"))
                    {
                        return v.WheelCount >= nr;
                    }
                    if (string.Equals(match.Groups["modifier"].Value, "-"))
                    {
                        return v.WheelCount <= nr;
                    }
                    return v.WheelCount == nr;
                };
            default:
                throw new ArgumentException($"'-{flag}' är inte en giltig flagga");
        }
    }

    private (char flag, string value) ParseFlag(string rawFlag)
    {
        string[] parts = rawFlag.Split(' ');
        if (parts.Length != 2)
        {
            throw new FormatException($"Flagga '{rawFlag}' är felformulerad.");
        }
        char flag = parts[0].Length > 1
            ? parts[0][1]
            : throw new FormatException($"Flagga '{rawFlag}' är felformulerad.");

        string value = parts[1].Trim();
        if (string.IsNullOrEmpty(value))
        {
            throw new FormatException($"Flagga '{rawFlag}' är felformulerad.");
        }
        return (flag, value);
    }
}