﻿﻿using Microsoft.Data.SqlClient;
using System.Data;

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
}