namespace UI;

public static class outputMessage
{
    /// <summary>
    /// Display a message
    /// </summary>
    public static void sucessConnexion(string firstname)
    {
        System.Console.WriteLine($"{firstname} connected successfully!");
    }

    public static void errorConnexion()
    {
        System.Console.WriteLine("Please check your information and try again!");
    }
}