using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testingLitedb
{
    class Program
    {
        static void Main(string[] args)
        {
            string userinput;

            Console.WriteLine("Create User      [1]");
            Console.WriteLine("Delete User Data [2]");
            Console.WriteLine("View User Data   [3]");            

            //user command input
            userinput = Console.ReadLine();

            switch (userinput)
            {
                case "1":
                    InsertData();
                    break; 
                case "2":
                    DeleteData();
                    break;
                case "3":
                    ViewData();
                    break;                    
                default:
                    break;
            }
            
        }

        static void InsertData()
        {
            string name = "";
            int age = 0;
            Console.Write("Input Name: ");
            name = Console.ReadLine();
            Console.Write("Input Age: ");
            age = Convert.ToInt32(Console.ReadLine());
                        
            using (var db = new LiteDatabase(@"testDB.db"))
            {
                //fetch input for new customer
                var collection = db.GetCollection<Customer>("customer");
                                               
                var customer = new Customer();
                customer.Name = name;
                customer.Age = age;

                collection.Insert(customer);
            }
        }
        static void ViewData()
        {
            using (var db = new LiteDatabase(@"testDb.db"))
            {
                //create collection for testdb
                var collection = db.GetCollection<Customer>("customer");
                IEnumerable<Customer> customers = collection.FindAll();

                Console.WriteLine("ID Name Age");

                foreach (var item in customers)
                {
                    Console.WriteLine("{0} {1} {2}", item.Id, item.Name, item.Age);
                }
            }
        }
        static void DeleteData()
        {
            int id;
            id = Convert.ToInt32(Console.ReadLine());
            using (var db = new LiteDatabase(@"testDB.db"))
            {
                var collection = db.GetCollection<Customer>("customer");

                collection.Delete(id);
                Console.WriteLine("Delete Item Success!");
            }
        }
    }
}
