using System.ComponentModel.DataAnnotations;

namespace Models;

public class User
{
    // private string idcustomer = "";
    // public string IDCustomer
    // {
    //     get => idcustomer;
    //     set
    //     {
    //         if (String.IsNullOrWhiteSpace(value))
    //         {
    //             throw new ValidationException("idcustomer cannot be empty");
    //         }
    //         idcustomer = value;
    //     }
    // }
    private string lastname = "";
    public string LastName
    {
        get => lastname;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("Last name cannot be empty");
            }
            lastname = value;
        }
    }
    private string firstname = "";
    public string FirstName
    {
        get => firstname;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("First name cannot be empty");
            }
            firstname = value;
        }
    }

    private string password = "";
    public int Password { get; private set; }
    private string username = "";
    public string UserName
    {
        get => username;
        set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ValidationException("username cannot be empty");
            }
            username = value;
        }
    }

}
