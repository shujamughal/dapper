// See https://aka.ms/new-console-template for more information
using dapper_lec2Console;
using Microsoft.Data.SqlClient;
using Dapper;

Console.WriteLine("relationships in dapper!");
string connectionString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=MyDB;Integrated Security=True;";

/* one to one relation example Users <--> UserProfiles */
var userId = 1;
using (var connection = new SqlConnection(connectionString))
{
    string sql = @"
        SELECT * FROM Users u
        LEFT JOIN UserProfiles p ON u.Id = p.Id
       -- WHERE u.Id = @UserId";

    var userDictionary = new Dictionary<int, User>();

    var users = connection.Query<User, UserProfile, User>(
        sql,
        (user, profile) =>
        { 
            if (profile != null)
            user.UserProfile = profile;
            return user;
        },
        new { UserId = userId },
        splitOn: "Id"
    ).ToList();
    foreach (var user in users)
    {
        Console.WriteLine($" {user.Name} : {user.Email}");
        if (user.UserProfile != null)
            Console.WriteLine(user.UserProfile.UserDetails);
    }
}

/* one to many relation examples Customer --> Orders         */
var customerId = 1;
using (var connection = new SqlConnection(connectionString))
{
    string sql = @"
        SELECT c.*, o.*
        FROM Customers c
        LEFT JOIN Orders o ON c.Id = o.CustomerId
        --WHERE c.Id = @Id";
    var customerLookup = new Dictionary<int, Customer>();

    connection.Query<Customer, Order, Customer>(
        sql,
        (customer, order) =>
        {
            if (!customerLookup.TryGetValue(customer.Id, out var customerEntry))
            {
                customerEntry = customer;
                customerEntry.Orders = new List<Order>();
                customerLookup.Add(customerEntry.Id, customerEntry);
            }

            if (order != null)
            {
                customerEntry.Orders.Add(order);
            }

            return customerEntry;
        },
        splitOn: "Id"
    );

    // Printing all customers with their orders
    foreach (var customer in customerLookup.Values)
    {
        Console.WriteLine($"Customer ID: {customer.Id}");
        Console.WriteLine($"Customer Name: {customer.CustomerName}");
        Console.WriteLine("Orders:");
        foreach (var order in customer.Orders)
        {
            Console.WriteLine($"\tOrder ID: {order.Id}, Order Date: {order.OrderDate}");
        }
        Console.WriteLine();
    }

}
    /* many to many relationship example  student and course */
 using (var connection = new SqlConnection(connectionString))
    {
        string sql = @"
                SELECT s.Id AS Id, s.Name AS Name,
                       c.Id AS Id, c.Name AS Name
                FROM Students s
                INNER JOIN StudentCourses sc ON s.Id = sc.StudentId
                INNER JOIN Courses c ON sc.CourseId = c.Id
                ORDER BY s.Id";

        var studentLookup = new Dictionary<int, Student>();

        connection.Query<Student, Course, Student>(
            sql,
            (student, course) =>
            {
                if (!studentLookup.TryGetValue(student.Id, out var existingStudent))
                {
                    existingStudent = student;
                    studentLookup.Add(existingStudent.Id, existingStudent);
                }

                if (course != null)
                {
                    existingStudent.Courses.Add(course);
                }

                return existingStudent;
            },
            splitOn: "Id"
        );

        // Printing all students with their courses
        foreach (var student in studentLookup.Values)
        {
            Console.WriteLine($"Student ID: {student.Id}");
            Console.WriteLine($"Student Name: {student.Name}");
            Console.WriteLine("Courses:");
            foreach (var course in student.Courses)
            {
                Console.WriteLine($"\tCourse ID: {course.Id}, Course Name: {course.Name}");
            }
            Console.WriteLine();
        }


    }
