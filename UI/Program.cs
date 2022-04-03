using UI;
using DL;
using BL;


string connectionString = File.ReadAllText("./connectionString.txt");

//Dependency Injection
IStoreDL repo = new StoreDL(connectionString);
IStoreBL bl = new StoreBL(repo);
new MainMenu(bl).Start();


//new MainMenu().Start();