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

    /// <summary>
    /// Prompts the user to select an option from a set of choices and returns the selected value.
    /// </summary>
    /// <typeparam name="T">The type of the values in the options dictionary.</typeparam>
    /// <param name="options">A dictionary where each key represents a selectable option and its corresponding value represents the associated
    /// data.</param>
    /// <param name="displayFunc">A function that generates a display string for each value in the options dictionary.</param>
    /// <param name="prompt">An optional message to display before listing the options. If null, no prompt is displayed.</param>
    /// <param name="errorMessage">An optional message to display when the user makes an invalid selection. Defaults to "Ogiltigt val, försök
    /// igen!" if not provided.</param>
    /// <returns>The value associated with the option selected by the user.</returns>
    public T PrintMenu<T>(IEnumerable<IMenuItem> menu, string? prompt = null, string? errorMessage = "Ogiltigt val, försök igen!");

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