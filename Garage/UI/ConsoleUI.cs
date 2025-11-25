
using GarageApp.Interfaces;
using System.Reflection;
using System.Text;

namespace GarageApp.UI;

public class ConsoleUI : IUI
{
    /// <inheritdoc/>
    public T? AskForInput<T>(string? prompt, string? errorMessage)
    {
        PrintLine(prompt);

        string? input = Console.ReadLine();

        try
        {
            return ParseValue<T>(input);
        }
        catch (NotSupportedException ex)
        {
            PrintErrorLine(ex.Message);
            return default;
        }
        catch (Exception)
        {
            PrintErrorLine(errorMessage);
            return AskForInput<T>(prompt, errorMessage);
        }
    }

    /// <inheritdoc/>
    public T SelectInput<T>(Dictionary<char, T> options, Func<T, string> displayFunc, string? prompt = null, string? errorMessage = "Ogiltigt val, försök igen!")
    {
        PrintLine(prompt);
        foreach (KeyValuePair<char, T> kvp in options)
        {
            string display = displayFunc(kvp.Value);
            PrintLine($"{kvp.Key}: {display}");
        }
        PrintEmptyLines();
        char selected = AskForInput<char>("Välj ett alternativ: ", errorMessage);
        if (options.ContainsKey(selected))
        {
            return options[selected];
        }
        else
        {
            PrintErrorLine(errorMessage);
            return SelectInput<T>(options, displayFunc, prompt, errorMessage);
        }
    }
    public void PrintMenu(IEnumerable<IMenuItem> menu, IMenuItem exitItem, string? prompt = null, string? errorMessage = "Ogiltigt val, försök igen!")
    {
        while (true)
        {
            PrintLine(prompt);
            foreach (IMenuItem item in menu)
            {
                Print($"{item.Key}: {item.Label}");
                Console.WriteLine(item.SubItems is null ? "" : "...");
            }
            PrintLine($"{exitItem.Key}: {exitItem.Label}");

            ConsoleKey selected = AskForInput<ConsoleKey>("", errorMessage);
            Console.Clear();
            if (selected == exitItem.Key)
            {
                return;
            }
            IMenuItem? selectedItem = menu.Where(item => item.Key == selected).FirstOrDefault();
            if (selectedItem is null)
            {
                PrintErrorLine(errorMessage);
                PrintMenu(menu, exitItem, prompt, errorMessage);
                return;
            }
            if (selectedItem.SubItems is not null)
            {
                PrintMenu(selectedItem.SubItems, exitItem, $"{selectedItem.Label}: {prompt}", errorMessage);
            }
            else if (selectedItem.Action is not null)
            {
                selectedItem.Action.Invoke();
                Console.WriteLine();
            }
            else
            {
                PrintErrorLine($"'{selectedItem.Label}' har inte någon funktion. Välj något annat");
                PrintMenu(menu, exitItem, prompt, errorMessage);
            }
        }
    }

    /// <inheritdoc/>
    public void Print(string? prompt)
    {
        Console.Write(prompt);
    }

    /// <inheritdoc/>
    /// <summary>
    /// Does nothing if prompt is null, empty, or whitespace.
    /// </summary>
    public void PrintLine(string? prompt)
    {
        if (!string.IsNullOrWhiteSpace(prompt))
        {
            Console.WriteLine(prompt);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Does nothing if prompt is null, empty, or whitespace.
    /// </summary>
    public void PrintErrorLine(string? prompt)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        PrintLine(prompt);
        Console.ResetColor();
    }

    /// <summary>
    /// Try to parse the input string to type T using T.TryParse.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException">Thrown when T doesn't have TryParse method</exception>
    /// <exception cref="FormatException">Thrown when input could not be parsed into T</exception>
    private static T ParseValue<T>(string? input)
    {
        // special case for strings
        Type type = typeof(T);
        if (type == typeof(string))
        {
            return (T)(object)(input ?? "");
        }
        if (type.IsEnum)
        {
            return (T)Enum.Parse(type, input ?? "", true);
        }

        // get TryParse(string, out T) method
        var tryParseMethod = type.GetMethod(
            "TryParse",
            BindingFlags.Public | BindingFlags.Static,
            null,
            new Type[] { typeof(string), type.MakeByRefType() },
            null
        );

        if (tryParseMethod == null)
        {
            // type T does not have a TryParse method
            throw new NotSupportedException(
                $"Type {type.Name} does not support TryParse."
            );
        }

        // create arguments for TryParse
        object?[] args = new object?[] { input, null };

        // call method: bool success = T.TryParse(input, out value)
        bool success = (bool)tryParseMethod.Invoke(null, args)!;
        if (success)
        {
            return (T)args[1]!; // args[1] holds the out result
        }
        throw new FormatException($"Could not parse '{input}' to {type.Name}.");
    }

    /// <inheritdoc/>
    public void PrintEmptyLines(int lineCount = 1)
    {
        for (int i = 0; i < lineCount; i++)
        {
            Console.WriteLine();
        }
    }

    /// <inheritdoc/>
    public string FormatSquare(string content)
    {
        StringBuilder sb = new StringBuilder();
        int length = content.Length + 6;
        sb.AppendLine("┌" + (new string('─', length)) + "┐");
        sb.AppendLine($"│   {content}   │");
        sb.AppendLine("└" + (new string('─', length)) + "┘");
        return sb.ToString();
    }

}
