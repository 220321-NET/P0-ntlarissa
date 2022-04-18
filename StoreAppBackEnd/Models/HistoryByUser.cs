namespace Models;
public class HistoryByUser
{


    public User Users { get; set; } = new User();
    public List<Order> Orders { get; set; } = new List<Order>();

    public override string ToString()
    {
        string qString = $"ID Customer: {Users.ID} || Fist Name: {Users.FirstName} || Last Name : {Users.LastName} \n";
        if (Orders.Count > 0)
        {
            foreach (Order orders in Orders)
            {
                qString += orders.ToString() + "\n           ================================\n";
            }
        }
        return qString;
    }
}