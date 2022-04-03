using UI;
using BL;
using DB;


string connectionString = File.ReadAllText("./connectionString.txt");

// //Dependency Injection
// IStoreDB repo = new StoreDB(connectionString);
// IStoreBL bl = new StoreBL(repo);
// new MainMenu(bl).Start();


new MainMenu().Start();