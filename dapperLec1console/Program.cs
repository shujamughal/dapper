using Dapper;
using dapperLec1console;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("-- Introduction to Dapper --");


string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=MyDB;Integrated Security=True;";
SqlConnection connection = new SqlConnection(connectionString);




        Console.WriteLine(" -- insertion -- ");
        User user = new User {  Name="shuja@112", Email="shuja@gmail.com"};
        string insertSql = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email)";
        // connection.Execute(insertSql, user);
        connection.Execute(insertSql, new { Name=user.Name, Email= user.Email });

/*
        
        
        Console.WriteLine("-- update --");
        User userToUpdate = new User { Id = 3, Name = "fahad2", Email = "fahad2@gmail.com" };
        string udpateSql = "UPDATE Users SET Name = @Name, Email = @Email WHERE Id = @Id";
        connection.Execute(udpateSql, userToUpdate);

        Console.WriteLine("-- delete --");
        int id = 2;
        string deleteSql = "DELETE FROM Users WHERE Id = @Id";
        connection.Execute(deleteSql, new { Id = id });





Console.WriteLine("-- Reading -- ");
string sql = "select * from users";
connection.Query<User>(sql).ToList().ForEach(x => Console.WriteLine(x.Name));

        Console.WriteLine("-- Reading single line-- ");
        int singleId = 1;
        string Singlesql = "SELECT * FROM Users WHERE Id = @Id";
        User?  u = connection.QuerySingleOrDefault<User>(Singlesql, new { Id = singleId });
        Console.WriteLine( u.Id);
        Console.WriteLine(u.Email);
        Console.WriteLine(u.Name);
*/
/*Console.WriteLine("-- Multi-Mapping Example -- ");

string multiMappingSql = @"SELECT o.Id, o.Product, o.Amount, o.UserId, 
                              u.Id, u.Name, u.Email 
                       FROM Orders o 
                       INNER JOIN Users u ON o.UserId = u.Id";

var orders = connection.Query<Order, User, Order>(multiMappingSql, (order, user) =>
{
    order.User = user; // Associate the User with the Order
    return order;      // Return the Order object
},splitOn: "Id");

foreach(Order order in orders)
{
    Console.WriteLine($"oder id:  {order.Id} user name: {order.User.Name} and product:  {order.Product}");
    
}
*/