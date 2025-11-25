
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
    public T PrintMenu<T>(IEnumerable<IMenuItem> menu, string? prompt = null, string? errorMessage = "Ogiltigt val, försök igen!")
    {
        throw new NotImplementedException();
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
        if (typeof(T) == typeof(string))
        {
            return (T)(object)(input ?? "");
        }

        // get TryParse(string, out T) method
        var tryParseMethod = typeof(T).GetMethod(
            "TryParse",
            BindingFlags.Public | BindingFlags.Static,
            null,
            new Type[] { typeof(string), typeof(T).MakeByRefType() },
            null
        );

        if (tryParseMethod == null)
        {
            // type T does not have a TryParse method
            throw new NotSupportedException(
                $"Type {typeof(T).Name} does not support TryParse."
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
        throw new FormatException($"Could not parse '{input}' to {typeof(T).Name}.");
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
