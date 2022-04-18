namespace Models;
public class HistoryByStore
{
    public StoreFront Stores { get; set; } = new StoreFront();
    public List<Order> Orders { get; set; } = new List<Order>();

     public override string ToString()
    {
        string qString = $"ID Store: {Stores.ID} || Store Name: {Stores.Name} || ZipCode : {Stores.ZipCode} \n\n";
       if(Orders.Count > 0)
        {
            foreach(Order orders in Orders)
            {
                qString += orders.ToString() + "\n           =======================================================================\n";
            }
        }
       return  qString;
    }
}