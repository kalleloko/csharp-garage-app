namespace GarageApp.Interfaces;

/// <summary>
/// Represents a user interface abstraction for interacting with users through input and output operations.
/// </summary>
/// <remarks>This interface provides methods for prompting the user for input, displaying messages, and formatting
/// content. It supports generic input handling, selection from predefined options, and error message
/// handling.</remarks>
internal interface IUI
{
    /// <summary>
    /// Prompts the user for input and parses the response into the specified type.
    /// </summary>
    /// <typeparam name="T">The type to which the user input will be converted.</typeparam>
    /// <param name="prompt">An optional message displayed to the user before accepting input. If null, no prompt is displayed.</param>
    /// <param name="errorMessage">An optional message displayed when the input cannot be parsed into the specified type. Defaults to "Ogiltigt
    /// värde, försök igen!".</param>
    /// <returns>The user input converted to the specified type <typeparamref name="T"/>.</returns>
    public T? AskForInput<T>(string? prompt = null, string? errorMessage = "Ogiltigt värde, försök igen!");

    public object? AskForInput(Type type, string? prompt = null, string? errorMessage = "Ogiltigt värde, försök igen!");

    /// <summary>
    /// From a list of menu items with associated actions, invoke the one that is selected by the user
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="exitItem"></param>
    /// <param name="prompt"></param>
    /// <param name="errorMessage"></param>
    /// <returns>True if the action was invoked, false if not</returns>
    public bool InvokeOneMenuAction(IEnumerable<IMenuItem> menu, IMenuItem exitItem, string prompt, string? errorMessage = "Ogiltigt val, försök igen!");
    /// <summary>
    /// 
    /// </summary>
    /// <param name="menu"></param>
    /// <param name="prompt"></param>
    /// <param name="errorMessage"></param>
    /// <returns></returns>
    public void PrintMenu(IEnumerable<IMenuItem> menu, IMenuItem exitItem, string prompt, string? errorMessage = "Ogiltigt val, försök igen!");

    /// <summary>
    /// Fill the entire width of the output with the provided char
    /// </summary>
    /// <param name="input"></param>
    public void PrintFullLine(char input = '-');

    /// <summary>
    /// Displays the specified prompt message to the user.
    /// </summary>
    /// <param name="prompt">The message to display.</param>
    public void Print(string? prompt);

    /// <summary>
    /// Writes the specified prompt followed by a newline to the output.
    /// </summary>
    /// <param name="prompt">The text to write to the output.</param>
    public void PrintLine(string? prompt);

    /// <summary>
    /// Writes the specified prompt, intended for error messages, followed by a newline to the output.
    /// </summary>
    /// <param name="prompt">The text to write to the output. </param>
    public void PrintErrorLine(string? prompt);

    /// <summary>
    /// Prints the specified number of empty lines to the console.
    /// </summary>
    /// <param name="lineCount">The number of empty lines to print. Defaults to 1 if not specified.</param>
    public void PrintEmptyLines(int lineCount = 1);

    /// <summary>
    /// Formats the specified content by applying a square-shaped layout.
    /// </summary>
    /// <param name="content">The text content to be formatted. Cannot be null or empty.</param>
    /// <returns>A string representing the content formatted in a square layout.</returns>
    public string FormatSquare(string content);

}