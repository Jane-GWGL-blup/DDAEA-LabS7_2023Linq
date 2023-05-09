using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Linq2023
{
    public class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();

        static List<Product> products = new List<Product>();
        static void Main(string[] args)
        {
            /*IntroToLINQ();
            DataSource();
            Filtering();
            Ordering();
            Grouping();
            Grouping2();
            Joining();*/

            Console.Read();

        }
        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery = 
                from num in numbers where (num % 2) == 0 select num;

            foreach(int num in numQuery)
            {
                Console.Write("{0,1} ", num);
            }
        }
        
        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes select cust;

            foreach(var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        
        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;

            foreach(var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void Ordering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "London"
                                       orderby cust.NombreCompañia ascending
                                       select cust;

            foreach(var item in queryLondonCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        
        static void Grouping()
        {
            var queryCustomersByCity = from cust in context.clientes
                                       group cust by cust.Ciudad;

            foreach(var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach(clientes customer in customerGroup)
                {
                    Console.WriteLine("      {0}", customer.NombreCompañia);
                }
            }
        }

        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;
            
            foreach(var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }

        static void Joining()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                                 select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            foreach(var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }


        private static void ShowPares(int[] numbers)
        {
            Console.WriteLine("ShowPares");
            var pares = (from c in numbers
                         where c % 2 == 0
                         select c).ToList();


            foreach (var par in pares) { Console.WriteLine(par); }
        }
        private static void ShowParesLambda(int[] numbers)
        {
            Console.WriteLine("ShowParesLambda");
            var pares = numbers.Where(x => x % 2 == 0).ToList();
            foreach (var par in pares) { Console.WriteLine(par); }
        }

        private static void InsertProducts()
        {
            string[] basicNeeds = { "Leche", "Pan", "Arroz", "Huevos", "Azúcar", "Aceite", "Sal", "Harina", "Pasta", "Jabón", "Papel higiénico", "Detergente", "Cepillo de dientes", "Shampoo", "Cebolla", "Zanahoria", "Papa", "Tomate", "Atún", "Pollo" };

            Random random = new Random();
            for (int i = 1; i <= 100; i++)
            {
                int productId = i;
                string name = basicNeeds[random.Next(0, basicNeeds.Length)];
                int price = random.Next(10, 100); // Genera un precio aleatorio entre 10 y 100
                products.Add(new Product { ProductId = productId, Name = name, Price = price });
            }
        }

    }
}