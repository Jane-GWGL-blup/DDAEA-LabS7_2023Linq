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

            /*IntroToLINQLambda();
             * DataSourceLambda();
             * FilteringLambda();
             * OrderingLambda();
             * GroupingLambda();
             * Grouping2Lambda();
             *   JoiningLambda();
             */

            JoiningLambda();

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
        static void IntroToLINQLambda()
        {

            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
            Console.WriteLine("ShowParesLambda");
            var pares = numbers.Where(x => x % 2 == 0).ToList();
            foreach (var par in pares) { Console.WriteLine(par); }
        }

        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes select cust;

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }

        static void DataSourceLambda()
        {
            var queryAllCustomers = context.clientes.Select(p => new { p.NombreCompañia}).ToList();


            foreach (var item in queryAllCustomers)
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

        static void FilteringLambda()
        {
            var queryLondonCustomers = context.clientes.Where(x => x.Ciudad == "Londres").ToList();

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }

        static void Ordering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       orderby cust.NombreCompañia ascending
                                       select cust;

            foreach(var item in queryLondonCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }


        static void OrderingLambda()
        {
            var queryLondonCustomers = context.clientes.Where(x => x.Ciudad == "Londres").OrderBy(x => x.NombreCompañia).ToList();
                                       

            foreach (var item in queryLondonCustomers)
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

        static void GroupingLambda()
        {
            var queryCustomersByCity = context.clientes.GroupBy(x => x.Ciudad).ToList();
                                    
            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
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

        static void Grouping2Lambda()
        {
            var custQuery = context.clientes.GroupBy(x => x.Ciudad).Where(x => x.Count() > 2 ).OrderBy(x => x.Key).ToList();
            //var custGroup = custQuery.Where(x => x.Count() > 2).OrderBy(x => x.Key).ToList();
            foreach (var item in custQuery)
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

        static void JoiningLambda()
        {
            var innerJoinQuery = context.clientes.Join(context.Pedidos, cliente => cliente.idCliente, pedido => pedido.IdCliente, (cliente, pedido) => new { CustomerName = cliente.NombreCompañia, DistributorName = pedido.PaisDestinatario }).ToList();
                 
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }

    }
}