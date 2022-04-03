namespace UI;

public static class InputValidation
{
    /// <summary>
    /// Check if string input is valid. An invalid string is null or whitespace;
    /// </summary>
    /// <returns>string</returns>
    public static string validString()
    {
    EnterString:
        string valid = Console.ReadLine() ?? "";

        if (String.IsNullOrWhiteSpace(valid))
        {
            Console.WriteLine("Enter a valid input: ");
            goto EnterString;
        }
        return valid;
    }
}