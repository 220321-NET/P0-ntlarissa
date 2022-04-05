namespace Models;

public class Addresses
{
    public int ID { get; set; }
    private string line1 = "";
    public string Line1
    {
        get { return line1; }
        set { line1 = value; }
    }
    private string line2 = "";
    public string Line2
    {
        get { return line2; }
        set { line2 = value; }
    }

    private string city = "";
    public string City
    {
        get { return city; }
        set { city = value; }
    }

    private string state = "";
    public string State
    {
        get { return state; }
        set { state = value; }
    }
    private string country = "";
    public string Country
    {
        get { return country; }
        set { country = value; }
    }
    private string zipCode = "";
    public string ZipCode
    {
        get { return zipCode; }
        set { zipCode = value; }
    }
    
}