namespace Models;
public class Order
{
    public float OrderTotal { get; set; }
    private string orderRef = "";
    public string OrderRef
    {
        get { return orderRef; }
        set { orderRef = value; }
    }

    public DateTime OrderDate { get; set; }
    
    public int CustomerID { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();

     public override string ToString()
    {
        string qString = $"Ref Order: {OrderRef} \nID Customer: {CustomerID} \nDate : {OrderDate.ToString()} ";
       if(Products.Count > 0)
        {
            qString += "\n Products: \n";
            foreach(Product prods in Products)
            {
                qString += prods.ToString() + "\n================================\n";
            }
        }
        qString += $"Total Order: {OrderTotal} ";
       return  qString;
    }
}