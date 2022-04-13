﻿using Microsoft.Data.SqlClient;
using System.Data;
using Models;
using System.ComponentModel.DataAnnotations;

namespace DL;
public class StoreDL : IStoreDL
{

    private readonly string _connectionString;


    /// <summary>
    /// define or initialise a connexion information
    /// </summary>
    public StoreDL(string connectionString)
    {
        _connectionString = connectionString;
    }

    // /// <summary>
    // /// check if customer exists in the dataset.
    // /// </summary>
    // /// <param name="userName">Username of customer.</param>
    // /// <param name="password">Password of customer.</param>
    // /// <returns>Return null or somes informations about the customer.</returns>

    // User? customerConnexion(string userName, string password)
    // {

    // }

    public User createNewUser(User userToCreate)
    {
        //add customer
        DataSet customerSet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE customerID = -1", connection);

        SqlDataAdapter customerAdapter = new SqlDataAdapter(cmd);

        customerAdapter.Fill(customerSet, "CustomerTable");

        DataTable? customerTable = customerSet.Tables["CustomerTable"];
        if (customerTable != null)
        {
            DataRow newRow = customerTable.NewRow();
            newRow["customerUserName"] = userToCreate.UserName;
            newRow["customerFirstName"] = userToCreate.FirstName;
            newRow["customerLastName"] = userToCreate.LastName;
            newRow["customerPassword"] = userToCreate.Password;

            customerTable.Rows.Add(newRow);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(customerAdapter);
            SqlCommand insert = commandBuilder.GetInsertCommand();

            customerAdapter.InsertCommand = insert;

            try
            {
                customerAdapter.Update(customerTable);
                return userToCreate;
            }
            catch (Exception)
            {
                return null!;
            }
        }
        return null!;
    }

    // public User createNewUser(User userToCreate)
    // {
    //     if (userToCreate.IsAdmin)
    //     {
    //         //add manager 
    //         DataSet managerSet = new DataSet();

    //         using SqlConnection connection = new SqlConnection(_connectionString);
    //         using SqlCommand cmd = new SqlCommand("SELECT * FROM Managers WHERE managerID = -1", connection);

    //         SqlDataAdapter managerAdapter = new SqlDataAdapter(cmd);

    //         managerAdapter.Fill(managerSet, "ManagerTable");

    //         DataTable? managerTable = managerSet.Tables["ManagerTable"];
    //         if (managerTable != null)
    //         {
    //             DataRow newRow = managerTable.NewRow();
    //             newRow["managerUserName"] = userToCreate.UserName;
    //             newRow["managerFirstName"] = userToCreate.FirstName;
    //             newRow["managerLastName"] = userToCreate.LastName;
    //             newRow["managerPassword"] = userToCreate.Password;
    //             newRow["storeID"] = userToCreate.IDStore;
    //             System.Console.WriteLine(userToCreate.IDStore);

    //             managerTable.Rows.Add(newRow);

    //             SqlCommandBuilder commandBuilder = new SqlCommandBuilder(managerAdapter);
    //             SqlCommand insert = commandBuilder.GetInsertCommand();

    //             managerAdapter.InsertCommand = insert;

    //             try
    //             {
    //                 managerAdapter.Update(managerTable);
    //                 return userToCreate;
    //             }
    //             catch (Exception ex)
    //             {
    //                 System.Console.WriteLine(ex);
    //                 return null!;
    //             }
    //         }
    //     }
    //     else
    //     {
    //         //add customer
    //         DataSet customerSet = new DataSet();

    //         using SqlConnection connection = new SqlConnection(_connectionString);
    //         using SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE customerID = -1", connection);

    //         SqlDataAdapter customerAdapter = new SqlDataAdapter(cmd);

    //         customerAdapter.Fill(customerSet, "CustomerTable");

    //         DataTable? customerTable = customerSet.Tables["CustomerTable"];
    //         if (customerTable != null)
    //         {
    //             DataRow newRow = customerTable.NewRow();
    //             newRow["customerUserName"] = userToCreate.UserName;
    //             newRow["customerFirstName"] = userToCreate.FirstName;
    //             newRow["customerLastName"] = userToCreate.LastName;
    //             newRow["customerPassword"] = userToCreate.Password;

    //             customerTable.Rows.Add(newRow);

    //             SqlCommandBuilder commandBuilder = new SqlCommandBuilder(customerAdapter);
    //             SqlCommand insert = commandBuilder.GetInsertCommand();

    //             customerAdapter.InsertCommand = insert;

    //             try
    //             {
    //                 customerAdapter.Update(customerTable);
    //                 return userToCreate;
    //             }
    //             catch (Exception)
    //             {
    //                 return null!;
    //             }
    //         }
    //     }
    //     return null!;
    // }

    public async Task<User> getUserAsync(string username)
    {
        return await Task.Factory.StartNew(() =>
                {
                    // if (userToGet.IsAdmin)
                    // {//get manager information
                    //     DataSet mangerSet = new DataSet();

                    //     using SqlConnection connection = new SqlConnection(_connectionString);
                    //     using SqlCommand cmd = new SqlCommand("SELECT * FROM Managers WHERE managerUserName = @username", connection);
                    //     cmd.Parameters.AddWithValue("@username", userToGet.UserName);

                    //     SqlDataAdapter mangerAdapter = new SqlDataAdapter(cmd);

                    //     mangerAdapter.Fill(mangerSet, "ManagerTable");

                    //     DataTable? mangerTable = mangerSet.Tables["ManagerTable"];
                    //     if (mangerTable != null && mangerTable.Rows.Count > 0 && userToGet.Password == (string)mangerTable.Rows[0]["managerPassword"])
                    //     {
                    //         userToGet.ID = (int)mangerTable.Rows[0]["managerID"];
                    //         userToGet.FirstName = (string)mangerTable.Rows[0]["managerFirstName"];
                    //         userToGet.LastName = (string)mangerTable.Rows[0]["managerLastName"];
                    //         userToGet.IDStore = (int)mangerTable.Rows[0]["storeID"];
                    // //userToGet.Status = 1;
                    //         return userToGet;
                    //     }
                    // }
                    // else
                    // {// get customer information

                    DataSet customerSet = new DataSet();
                    User userToGet = new User();
                    using SqlConnection connection = new SqlConnection(_connectionString);
                    using SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE customerUserName = @username", connection);
                    cmd.Parameters.AddWithValue("@username", username);

                    SqlDataAdapter customerAdapter = new SqlDataAdapter(cmd);

                    customerAdapter.Fill(customerSet, "CustomerTable");

                    DataTable? customerTable = customerSet.Tables["CustomerTable"];
                    if (customerTable != null && customerTable.Rows.Count > 0)
                    {
                        userToGet.ID = (int)customerTable.Rows[0]["customerID"];
                        userToGet.FirstName = (string)customerTable.Rows[0]["customerFirstName"];
                        userToGet.LastName = (string)customerTable.Rows[0]["customerLastName"];
                        userToGet.Password = (string)customerTable.Rows[0]["customerPassword"];
                        userToGet.UserName = username;
                        //userToGet.Status = 1;
                        return userToGet;
                    }

                    // }
                    return null!;
                });
    }
    // public StoreFront addStoreFront(StoreFront storeToAdd)
    // {
    //     //add store
    //     DataSet storeSet = new DataSet();

    //     using SqlConnection connection = new SqlConnection(_connectionString);
    //     using SqlCommand cmd = new SqlCommand("SELECT * FROM StoreFront WHERE storeID = -1", connection);

    //     SqlDataAdapter storeAdapter = new SqlDataAdapter(cmd);

    //     storeAdapter.Fill(storeSet, "StoreTable");

    //     DataTable? storeTable = storeSet.Tables["StoreTable"];
    //     if (storeTable != null)
    //     {
    //         DataRow newRow = storeTable.NewRow();
    //         newRow["storeName"] = storeToAdd.Name;
    //         newRow["addressLine1"] = storeToAdd.Line1;
    //         newRow["addressLine2"] = storeToAdd.Line2;
    //         newRow["addressCity"] = storeToAdd.City;
    //         newRow["addressState"] = storeToAdd.State;
    //         newRow["addressCountry"] = storeToAdd.Country;
    //         newRow["addressZipCode"] = storeToAdd.ZipCode;

    //         storeTable.Rows.Add(newRow);

    //         SqlCommandBuilder commandBuilder = new SqlCommandBuilder(storeAdapter);
    //         SqlCommand insert = commandBuilder.GetInsertCommand();

    //         storeAdapter.InsertCommand = insert;

    //         try
    //         {
    //             storeAdapter.Update(storeTable);
    //             return storeToAdd;
    //         }
    //         catch (Exception)
    //         {
    //             return null!;
    //         }
    //     }
    //     return null!;
    // }


    public Product addProduct(Product productToAdd)
    {

        //add product
        DataSet productSet = new DataSet();

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE productID = -1", connection);

        SqlDataAdapter productAdapter = new SqlDataAdapter(cmd);

        productAdapter.Fill(productSet, "ProductTable");

        DataTable? productTable = productSet.Tables["ProductTable"];
        if (productTable != null)
        {
            DataRow newRow = productTable.NewRow();
            newRow["productQuantity"] = productToAdd.ProductQuantity;
            newRow["productPrice"] = productToAdd.ProductPrice;
            newRow["productRef"] = productToAdd.ProductRef;
            newRow["productName"] = productToAdd.NameProduct;
            newRow["storeID"] = productToAdd.IDStore;

            productTable.Rows.Add(newRow);

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(productAdapter);
            SqlCommand insert = commandBuilder.GetInsertCommand();

            productAdapter.InsertCommand = insert;

            try
            {
                productAdapter.Update(productTable);
                return productToAdd;
            }
            catch (Exception)
            {
                return null!;
            }
        }
        return null!;

    }
    // public List<Product> GetAllProductByStore(int store)
    // {
    //     List<Product> products = new List<Product>();
    //     DataSet productSet = new DataSet();

    //     using SqlConnection connection = new SqlConnection(_connectionString);
    //     using SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE storeID = @store", connection);
    //     cmd.Parameters.AddWithValue("@store", store);

    //     SqlDataAdapter productAdapter = new SqlDataAdapter(cmd);

    //     productAdapter.Fill(productSet, "ProductTable");

    //     DataTable? ProductTable = productSet.Tables["ProductTable"];
    //     if (ProductTable != null && ProductTable.Rows.Count > 0)
    //     {
    //         foreach (DataRow row in ProductTable.Rows)
    //         {
    //             Product product = new Product();

    //             product.IDProduct = (int)row["productID"];
    //             product.NameProduct = (string)row["productName"];
    //             product.ProductRef = (string)row["productRef"];
    //             product.ProductQuantity = Convert.ToSingle(row["ProductQuantity"]);
    //             product.ProductPrice = Convert.ToSingle(row["productPrice"]);
    //             products.Add(product);
    //         }
    //         return products;
    //     }
    //     return null!;
    // }
    // public Product updateProduct(Product productToUpdate)
    // {
    //     DataSet productSet = new DataSet();

    //     using SqlConnection connection = new SqlConnection(_connectionString);
    //     using SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE productID = @id", connection);
    //     cmd.Parameters.AddWithValue("@id", productToUpdate.IDProduct);

    //     SqlDataAdapter productAdapter = new SqlDataAdapter(cmd);

    //     productAdapter.Fill(productSet, "ProductTable");

    //     DataTable? productTable = productSet.Tables["ProductTable"];
    //     if (productTable != null && productTable.Rows.Count > 0)
    //     {
    //         DataColumn[] dt = new DataColumn[1];
    //         dt[0] = productTable.Columns["productID"]!;
    //         productTable.PrimaryKey = dt;
    //         DataRow? rowToUpdate = productTable.Rows.Find(productToUpdate.IDProduct);
    //         if (rowToUpdate != null)
    //         {
    //             rowToUpdate["productQuantity"] = productToUpdate.ProductQuantity;
    //             rowToUpdate["productPrice"] = productToUpdate.ProductPrice;
    //             rowToUpdate["productRef"] = productToUpdate.ProductRef;
    //             rowToUpdate["productName"] = productToUpdate.NameProduct;
    //         }

    //         SqlCommandBuilder commandBuilder = new SqlCommandBuilder(productAdapter);
    //         SqlCommand updateCmd = commandBuilder.GetUpdateCommand();

    //         productAdapter.UpdateCommand = updateCmd;
    //         productAdapter.Update(productTable);
    //         return productToUpdate;
    //     }
    //     return null!;
    // }

    public async Task<List<Product>> GetAllProductAsync()
    {
        return await Task.Factory.StartNew(() =>
                {
                    List<Product> products = new List<Product>();
                    DataSet productSet = new DataSet();

                    using SqlConnection connection = new SqlConnection(_connectionString);
                    using SqlCommand cmd = new SqlCommand("SELECT * FROM Products", connection);

                    SqlDataAdapter productAdapter = new SqlDataAdapter(cmd);

                    productAdapter.Fill(productSet, "ProductTable");

                    DataTable? ProductTable = productSet.Tables["ProductTable"];
                    if (ProductTable != null && ProductTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in ProductTable.Rows)
                        {
                            Product product = new Product();

                            product.IDProduct = (int)row["productID"];
                            product.IDStore = (int)row["storeID"];
                            product.NameProduct = (string)row["productName"];
                            product.ProductRef = (string)row["productRef"];
                            product.ProductQuantity = Convert.ToSingle(row["ProductQuantity"]);
                            product.ProductPrice = Convert.ToSingle(row["productPrice"]);
                            if (product.ProductQuantity > 0)
                            {
                                products.Add(product);
                            }

                        }
                        return products;
                    }
                    return null!;
                });
    }


    // public Order placeOrder(Order orderToPlace)
    // {

    //     //add order
    //     DataSet orderSet = new DataSet();

    //     using SqlConnection connection = new SqlConnection(_connectionString);
    //     using SqlCommand cmd = new SqlCommand("SELECT * FROM Orders WHERE orderID = -1", connection);

    //     SqlDataAdapter orderAdapter = new SqlDataAdapter(cmd);

    //     orderAdapter.Fill(orderSet, "OrderTable");

    //     DataTable? orderTable = orderSet.Tables["OrderTable"];
    //     if (orderTable != null)
    //     {
    //         foreach (Product prods in orderToPlace.Products)
    //         {
    //             DataRow newRow = orderTable.NewRow();
    //             newRow["quantity"] = prods.ProductQuantity;
    //             newRow["price"] = prods.ProductPrice;
    //             newRow["orderDate"] = orderToPlace.OrderDate;
    //             newRow["orderRef"] = orderToPlace.OrderRef;
    //             newRow["total"] = orderToPlace.OrderTotal;
    //             newRow["productID"] = prods.IDProduct;
    //             newRow["productName"] = prods.NameProduct;
    //             newRow["productRef"] = prods.ProductRef;
    //             newRow["customerID"] = orderToPlace.CustomerID;
    //             orderTable.Rows.Add(newRow);
    //             if (!updateProductQty(prods.IDProduct, prods.ProductQuantity))
    //             {
    //                 return null!;
    //             }
    //         }

    //         SqlCommandBuilder commandBuilder = new SqlCommandBuilder(orderAdapter);
    //         SqlCommand insert = commandBuilder.GetInsertCommand();

    //         orderAdapter.InsertCommand = insert;

    //         try
    //         {
    //             orderAdapter.Update(orderTable);
    //             return orderToPlace;
    //         }
    //         catch (Exception)
    //         {
    //             return null!;
    //         }
    //     }
    //     return null!;
    // }

    // private bool updateProductQty(int id, float qty)
    // {
    //     float newQty = getProductQty(id) - qty;
    //     if (newQty < 0)
    //     {
    //         return false;
    //     }
    //     DataSet productSet = new DataSet();

    //     using SqlConnection connection = new SqlConnection(_connectionString);
    //     using SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE productID = @id", connection);
    //     cmd.Parameters.AddWithValue("@id", id);

    //     SqlDataAdapter productAdapter = new SqlDataAdapter(cmd);

    //     productAdapter.Fill(productSet, "ProductTable");

    //     DataTable? productTable = productSet.Tables["ProductTable"];
    //     if (productTable != null && productTable.Rows.Count > 0)
    //     {
    //         DataColumn[] dt = new DataColumn[1];
    //         dt[0] = productTable.Columns["productID"]!;
    //         productTable.PrimaryKey = dt;
    //         DataRow? rowToUpdate = productTable.Rows.Find(id);
    //         if (rowToUpdate != null)
    //         {
    //             rowToUpdate["productQuantity"] = newQty;
    //         }

    //         SqlCommandBuilder commandBuilder = new SqlCommandBuilder(productAdapter);
    //         SqlCommand updateCmd = commandBuilder.GetUpdateCommand();

    //         productAdapter.UpdateCommand = updateCmd;
    //         productAdapter.Update(productTable);
    //         return true;
    //     }
    //     return false;
    // }

    // private float getProductQty(int id)
    // {
    //     float qty = 0;
    //     DataSet productSet = new DataSet();

    //     using SqlConnection connection = new SqlConnection(_connectionString);
    //     using SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE productID = @id", connection);
    //     cmd.Parameters.AddWithValue("@id", id);

    //     SqlDataAdapter productAdapter = new SqlDataAdapter(cmd);

    //     productAdapter.Fill(productSet, "ProductTable");

    //     DataTable? productTable = productSet.Tables["ProductTable"];
    //     if (productTable != null && productTable.Rows.Count > 0)
    //     {
    //         qty = Convert.ToSingle(productTable.Rows[0]["productQuantity"]);
    //         return qty;
    //     }
    //     return qty;
    // }
    // public List<Order> getHistoryOrder(int id)
    // {
    //     List<Order> gethistory = new List<Order>();
    //     DataSet orderSet = new DataSet();

    //     using SqlConnection connection = new SqlConnection(_connectionString);
    //     using SqlCommand cmd = new SqlCommand("SELECT * FROM Orders WHERE customerID = @id ORDER BY orderRef", connection);
    //     cmd.Parameters.AddWithValue("@id", id);

    //     SqlDataAdapter orderAdapter = new SqlDataAdapter(cmd);

    //     orderAdapter.Fill(orderSet, "OrderTable");

    //     DataTable? orderTable = orderSet.Tables["OrderTable"];
    //     if (orderTable != null && orderTable.Rows.Count > 0)
    //     {
    //         string orderref;
    //         int position;
    //         for (int i = 0; i < orderTable.Rows.Count; i++)
    //         {

    //             orderref = (string)orderTable.Rows[i]["orderRef"];
    //             position = gethistory.FindIndex(x => x.OrderRef == orderref);
    //             if (position == -1) //ref order doest not exist in the list 
    //             {
    //                 Order order = new Order();
    //                 Product product = new Product();
    //                 order.CustomerID = id;
    //                 order.OrderDate = (DateTime)orderTable.Rows[i]["orderDate"];
    //                 order.OrderRef = (string)orderTable.Rows[i]["orderRef"];
    //                 product.ProductQuantity = Convert.ToSingle(orderTable.Rows[i]["quantity"]);
    //                 product.ProductPrice = Convert.ToSingle(orderTable.Rows[i]["price"]);
    //                 order.OrderTotal = Convert.ToSingle(orderTable.Rows[i]["total"]);
    //                 product.IDProduct = (int)orderTable.Rows[i]["productID"];
    //                 product.NameProduct = (string)orderTable.Rows[i]["productName"];
    //                 product.ProductRef = (string)orderTable.Rows[i]["productRef"];
    //                 order.Products.Add(product);
    //                 gethistory.Add(order);
    //             }
    //             else
    //             {
    //                 Product product = new Product();
    //                 product.ProductQuantity = Convert.ToSingle(orderTable.Rows[i]["quantity"]);
    //                 product.ProductPrice = Convert.ToSingle(orderTable.Rows[i]["price"]);
    //                 product.IDProduct = (int)orderTable.Rows[i]["productID"];
    //                 product.NameProduct = (string)orderTable.Rows[i]["productName"];
    //                 product.ProductRef = (string)orderTable.Rows[i]["productRef"];
    //                 gethistory[position].Products.Add(product);
    //             }

    //         }
    //         return gethistory;
    //     }
    //     return null!;
    // }

    // public List<User> GetAllCustomer()
    // {
    //     List<User> customers = new List<User>();
    //     DataSet customerSet = new DataSet();

    //     using SqlConnection connection = new SqlConnection(_connectionString);
    //     using SqlCommand cmd = new SqlCommand("SELECT * FROM Customers", connection);

    //     SqlDataAdapter customerAdapter = new SqlDataAdapter(cmd);

    //     customerAdapter.Fill(customerSet, "CustomerTable");

    //     DataTable? CustmomerTable = customerSet.Tables["CustomerTable"];
    //     if (CustmomerTable != null && CustmomerTable.Rows.Count > 0)
    //     {
    //         foreach (DataRow row in CustmomerTable.Rows)
    //         {
    //             User customer = new User();
    //             customer.ID = (int)row["customerID"];
    //             customer.FirstName = (string)row["customerFirstName"];
    //             customer.LastName = (string)row["customerLastName"];
    //             customer.UserName = (string)row["customerUserName"];
    //             customers.Add(customer);
    //         }
    //         return customers;
    //     }
    //     return null!;
    // }
}
