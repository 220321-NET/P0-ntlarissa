namespace UI;

public static class OutputMessage
{
    /// <summary>
    /// Display a message
    /// </summary>
    public static void SucessConnexion(string firstname)
    {
        System.Console.WriteLine($"{firstname} connected successfully!");
    }

    public static void ErrorConnexion()
    {
        System.Console.WriteLine("Please check your information and try again!");
    }
    public static void SucessCreation(string firstname)
    {
        System.Console.WriteLine($"{firstname} created successfully!");
    }

    public static void SucessUpdate(string firstname)
    {
        System.Console.WriteLine($"{firstname} updated successfully!");
    }

    public static void SucessCreationStore()
    {
        System.Console.WriteLine("New store created successfully!");
    }

    public static void ErrorCreation()
    {
        System.Console.WriteLine("Something wrong, please change your username and try again!");
    }
    public static void ErrorOperation()
    {
        System.Console.WriteLine("Something wrong, please check and try again!");
    }
}