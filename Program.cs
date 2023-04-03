using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;


namespace SQLandORM
{
    class Program
    {
        static void Main(string[] args)
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");


            IDbConnection conn = new MySqlConnection(connString);
            DapperDepartmentRepository repo = new DapperDepartmentRepository(conn);
            DapperProductRepository productRepository = new DapperProductRepository(conn);
            productRepository.CreateProduct("newItem", 20, 1);

            Console.WriteLine("Hello! Please press 'Enter' to view the current departments and products:");
            Console.ReadLine();

            var depos = repo.GetAllDepartments();
            Print(depos); 
            var prods = productRepository.GetAllProducts();
            Print(prods);



            Console.WriteLine("Would you like to add a department?");
            string userResponse = Console.ReadLine();

            if (userResponse.ToLower() == "yes")
            {
                Console.WriteLine("What would you like to name this new department?");
                userResponse = Console.ReadLine();

                repo.InsertDepartment(userResponse);
                Print(repo.GetAllDepartments());
            }
                    
               
            Console.WriteLine("Goodbye!");
        }

        private static void Print(IEnumerable<Department> depos)
        {
            foreach (var depo in depos)
            {
                Console.WriteLine($"Id: {depo.DepartmentID} // Name: {depo.Name}");
            }
        }

        private static void Print(IEnumerable<Product> prods)
        {
            foreach (var prod in prods)
            {
                Console.WriteLine($"CategoryId: {prod.CategoryID} // Name: {prod.Name}");
            }
        }
    }
}
