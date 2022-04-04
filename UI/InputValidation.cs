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
/// <summary>
    /// Check if int  input is valid.
    /// </summary>
    /// <returns>int</returns>
    public static int validIntPositif()
    {
        int valid;
    EnterInt:
        var validSring = Console.ReadLine();

        if (int.TryParse(validSring, out valid) == false || valid < 1)
        {
            Console.WriteLine("Enter a positif number: ");
            goto EnterInt;
        }
        return valid;
    }
}