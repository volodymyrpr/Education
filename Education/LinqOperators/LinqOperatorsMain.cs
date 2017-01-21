﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.LinqOperators
{
    class LinqOperatorsMain : IExecutable
    {
        string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };

        NutshellContext dataContext = new NutshellContext();

        public void Execute()
        {
            JoinExecute();
        }

        private void WhereExecute()
        {
            var query = names.Where(name => name.EndsWith("y"));

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var queryQS = from name in names
                          where name.Length > 3
                          let nameUpper = name.ToUpper()
                          where nameUpper.EndsWith("Y")
                          select nameUpper;

            foreach (var name in queryQS)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var queryIndexed = names.Where((name, position) => position % 2 == 0);

            foreach (var name in queryIndexed)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var queryStrangeFiltering = dataContext
                .Customers
                .Where(customer => SqlMethods.Like(customer.Name, "%r%y%"))
                .Select(customer => customer.Name);

            foreach (var name in queryStrangeFiltering)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var queryStrangeFiltering2 = dataContext
                .Customers
                .Where(customer => customer.Name.CompareTo("J") < 0)
                .Select(customer => customer.Name);

            foreach (var name in queryStrangeFiltering2)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            string[] chooseOnes = { "Tom", "Jay" };

            var queryStrangeFiltering3 = dataContext
                .Customers
                .Where(customer => chooseOnes.Contains(customer.Name))
                .Select(customer => customer.Name);

            foreach (var name in queryStrangeFiltering3)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
        }

        private void TakeSkipExecute()
        {
            int groupSize = 2;
            int numberOfGroups = dataContext.Customers.Count() / 2 + (dataContext.Customers.Count() % 2 == 0 ? 0 : 1);

            for (int groupNumber = 0; groupNumber < numberOfGroups; groupNumber++)
            {
                var currentGroup = dataContext
                    .Customers
                    .Skip(groupNumber * groupSize)
                    .Take(groupSize)
                    .Select(customer => customer.Name)
                    .ToList();

                foreach(var groupElement in currentGroup)
                {
                    Console.Write(groupElement);

                    if (groupElement != currentGroup.Last())
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine();
            }
        }

        private void TakeWhileSkipWhileExecute()
        {
            Console.WriteLine("Less than 4 digits long: ");
            var namesOrderedByLength = names.OrderBy(name => name.Length);

            foreach(var name in namesOrderedByLength.TakeWhile(name => name.Length < 4))
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("More than 3 digits long: ");

            foreach(var name in namesOrderedByLength.SkipWhile(name => name.Length < 4))
            {
                Console.WriteLine(name);
            }
        }

        private void DistinctExecute()
        {
            char[] letters = { };

            foreach(var name in names)
            {
                letters = letters.Concat(name.ToArray()).ToArray();
            }

            foreach(var letter in letters)
            {
                Console.Write(letter);
            }
            Console.WriteLine();

            foreach(var letter in letters.Distinct())
            {
                Console.Write(letter);
            }
        }

        private void SelectExecute()
        {
            var namesDuplicated = names.Select((name, position) => new { Position = position, TheName = name, FirstLetter = name[0]});

            foreach(var dupName in namesDuplicated)
            {
                Console.WriteLine(dupName.Position + ": " +dupName.TheName + " " + dupName.FirstLetter);
            }
            Console.WriteLine();

            DirectoryInfo[] dirs = new DirectoryInfo(@"E:\").GetDirectories();

            var query =
                from d in dirs
                where (d.Attributes & FileAttributes.System) == 0
                select new
                {
                    DirectoryName = d.FullName,
                    Created = d.CreationTime,

                    Files = from f in d.GetFiles()
                            where ((f.Attributes & FileAttributes.Hidden) == 0
                                && (f.Attributes & FileAttributes.ReadOnly) == 0 )
                            select new { FileName = f.Name, f.Length, }
                };

            foreach(var dirFiles in query)
            {
                Console.WriteLine("Directory: " + dirFiles.DirectoryName);
                foreach(var file in dirFiles.Files)
                {
                    Console.WriteLine(" " + file.FileName + " Len: " + file.Length);
                }
            }
            Console.WriteLine();

            //var queryToDb = dataContext.Customers.Select(customer => new
            //{
            //    customer.Name,
            //    Purchases = from purchase in dataContext.Purchases
            //                where purchase.CustomerID == customer.ID
            //                select new {purchase.Description, purchase.Price}
            //});

            var queryToDb = dataContext.Customers.Select(customer => new
            {
                customer.Name,
                Purchases = from purchase in customer.Purchases
                            where purchase.Price > 1000
                            select new { purchase.Description, purchase.Price }
            }).Where(custWithPurch => custWithPurch.Purchases.Count() > 0);

            foreach (var customer in queryToDb)
            {
                Console.WriteLine(customer.Name + ": ");

                foreach(var purchase in customer.Purchases)
                {
                    Console.WriteLine(purchase.Description + ", " + purchase.Price);
                }
            }
            Console.WriteLine();
        }

        private void SelectManyExecute()
        {
            string[] fullNames = { "Anne Williams", "Sue Green", "John Fred Smith" };

            var query = fullNames.SelectMany(fullName => fullName.Split(' '));

            foreach (var name in query)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var query2 = from fullName in fullNames
                         from name in fullName.Split().Select(name => new { Name = name, Source = fullName })
                         orderby name.Source, name.Name
                         select name.Name + " come from " + name.Source;

            foreach(var name in query2)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var query3 = fullNames
                .SelectMany(fullName => fullName.Split().Select(name => new { Name = name, Source = fullName }))
                .OrderBy(name => name.Source)
                .ThenBy(name => name.Name)
                .Select(name => name.Name + " come from " + name.Source);


            foreach(var name in query3)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var numbers = new string[] { "1", "2", "3" };
            var letters = new string[] { "a", "b" };

            var combinations = from n in numbers
                               from l in letters
                               select n + l;

            foreach(var combination in combinations)
            {
                Console.WriteLine(combination);
            }
            Console.WriteLine();

            var players = new string[] { "Petro", "Ivan", "Stepan" };
            var pairs = from player1 in players
                        from player2 in players
                        where player1.CompareTo(player2) > 0
                        select player1 + " and " + player2;

            foreach(var pair in pairs)
            {
                Console.WriteLine(pair);
            }
            Console.WriteLine();
        }

        private void JoinExecute()
        {
            var query =
                from c in dataContext.Customers
                join p in dataContext.Purchases
                    on c.ID equals p.CustomerID
                select c.Name + " bought a " + p.Description;

            foreach(var element in query)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            Customer[] customers = dataContext.Customers.ToArray();
            Purchase[] purchases = dataContext.Purchases.ToArray();
            var slowQuery = from c in customers
                            from p in purchases
                            where c.ID == p.CustomerID
                            select c.Name + " bought " + p.Description;

            foreach(var element in slowQuery)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            var query2 = from c in customers
                         join p in purchases
                            on c.ID equals p.CustomerID
                         select new { c.Name, p.Description, p.Price };

            foreach(var element in query2)
            {
                Console.WriteLine(element.Name + " " + element.Description + " " + element.Price);
            }
            Console.WriteLine();

            var query2Duplicate = customers.Join(
                purchases,
                c => c.ID,
                p => p.CustomerID,
                (c, p) => new { c.Name, p.Description, p.Price });

            foreach (var element in query2Duplicate)
            {
                Console.WriteLine(element.Name + " " + element.Description + " " + element.Price);
            }
            Console.WriteLine();
        }
    }
}
