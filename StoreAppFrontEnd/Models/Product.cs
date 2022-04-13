namespace Models;

public class Product
{

    public int IDProduct { get; set; }
    public int IDStore { get; set; }
    private string productRef = "";
    public string ProductRef
    {
        get { return productRef; }
        set { productRef = value; }
    }
    private string nameProduct = "";
    public string NameProduct
    {
        get { return nameProduct; }
        set { nameProduct = value; }
    }

public float ProductQuantity { get;  set; }
public float ProductPrice { get;  set; }

 public override string ToString()
    {
        string qString = $"ID : {IDProduct}  || Name : {NameProduct}   || Description : {ProductRef} || Quantity: {ProductQuantity} || Price: {ProductPrice}";
       return  qString;
    }
}