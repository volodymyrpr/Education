using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
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
            ExecuteGenerationMethods();
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

                foreach (var groupElement in currentGroup)
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

            foreach (var name in namesOrderedByLength.TakeWhile(name => name.Length < 4))
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("More than 3 digits long: ");

            foreach (var name in namesOrderedByLength.SkipWhile(name => name.Length < 4))
            {
                Console.WriteLine(name);
            }
        }

        private void DistinctExecute()
        {
            char[] letters = { };

            foreach (var name in names)
            {
                letters = letters.Concat(name.ToArray()).ToArray();
            }

            foreach (var letter in letters)
            {
                Console.Write(letter);
            }
            Console.WriteLine();

            foreach (var letter in letters.Distinct())
            {
                Console.Write(letter);
            }
        }

        private void SelectExecute()
        {
            var namesDuplicated = names.Select((name, position) => new { Position = position, TheName = name, FirstLetter = name[0] });

            foreach (var dupName in namesDuplicated)
            {
                Console.WriteLine(dupName.Position + ": " + dupName.TheName + " " + dupName.FirstLetter);
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
                                && (f.Attributes & FileAttributes.ReadOnly) == 0)
                            select new { FileName = f.Name, f.Length, }
                };

            foreach (var dirFiles in query)
            {
                Console.WriteLine("Directory: " + dirFiles.DirectoryName);
                foreach (var file in dirFiles.Files)
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

                foreach (var purchase in customer.Purchases)
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

            foreach (var name in query2)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var query3 = fullNames
                .SelectMany(fullName => fullName.Split().Select(name => new { Name = name, Source = fullName }))
                .OrderBy(name => name.Source)
                .ThenBy(name => name.Name)
                .Select(name => name.Name + " come from " + name.Source);


            foreach (var name in query3)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            var numbers = new string[] { "1", "2", "3" };
            var letters = new string[] { "a", "b" };

            var combinations = from n in numbers
                               from l in letters
                               select n + l;

            foreach (var combination in combinations)
            {
                Console.WriteLine(combination);
            }
            Console.WriteLine();

            var players = new string[] { "Petro", "Ivan", "Stepan" };
            var pairs = from player1 in players
                        from player2 in players
                        where player1.CompareTo(player2) > 0
                        select player1 + " and " + player2;

            foreach (var pair in pairs)
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

            foreach (var element in query)
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

            foreach (var element in slowQuery)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            var query2 = from c in customers
                         join p in purchases
                            on c.ID equals p.CustomerID
                         orderby p.Price descending
                         select new { c.Name, p.Description, p.Price };

            foreach (var element in query2)
            {
                Console.WriteLine(element.Name + " " + element.Description + " " + element.Price);
            }
            Console.WriteLine();

            var query2Duplicate = customers
                .Join(
                    purchases,
                    c => c.ID,
                    p => p.CustomerID,
                    (c, p) => new { c, p })
                .OrderByDescending(element => element.p.Price)
                .Select(element => new { element.c.Name, element.p.Description, element.p.Price });

            foreach (var element in query2Duplicate)
            {
                Console.WriteLine(element.Name + " " + element.Description + " " + element.Price);
            }
            Console.WriteLine();
        }

        private void ExecuteGroupJoin()
        {
            Customer[] customers = dataContext.Customers.ToArray();
            Purchase[] purchases = dataContext.Purchases.ToArray();

            var query = from c in customers
                        join p in purchases.Where(purchase => purchase.Price > 1000)
                            on c.ID equals p.CustomerID
                        into groupResult
                        where groupResult.Any()
                        select new { CustName = c.Name, Purchases = groupResult };

            foreach (var element in query)
            {
                Console.WriteLine(element.CustName + ":");
                foreach (var subElement in element.Purchases)
                {
                    Console.WriteLine(subElement.Description + " " + subElement.Price);
                }
            }
            Console.WriteLine();

            var queryGroupedJoinToFlat =
                from c in customers
                join p in purchases
                    on c.ID equals p.CustomerID
                into custPurchases
                from cp in custPurchases.DefaultIfEmpty()
                select new
                {
                    CustName = c.Name,
                    Price = cp == null ? (decimal?)null : cp.Price
                };

            foreach (var element in queryGroupedJoinToFlat)
            {
                Console.WriteLine(element.CustName + " " + element.Price);
            }
            Console.WriteLine();
        }

        private void ExecuteLookup()
        {
            Customer[] customers = dataContext.Customers.ToArray();
            Purchase[] purchases = dataContext.Purchases.ToArray();

            var purchLookup = purchases.ToLookup(p => p.CustomerID, p => p);

            foreach (var lookupElement in purchLookup[1])
            {
                Console.WriteLine(lookupElement.Description);
            }
            Console.WriteLine();

            var selectManyExample =
                from c in customers
                from p in purchLookup[c.ID].DefaultIfEmpty()
                select new
                {
                    CustName = c.Name,
                    Description = p == null ? null : p.Description,
                    Price = p == null ? null : (decimal?)p.Price
                };

            foreach (var purchase in selectManyExample)
            {
                Console.WriteLine(purchase.CustName + " " + purchase.Description + " " + purchase.Price);
            }
            Console.WriteLine();

            var query = from c in customers
                        select new
                        {
                            CustName = c.Name,
                            CustPurchases = purchLookup[c.ID]
                        };
        }

        private void ExecuteZip()
        {
            int[] numbers = { 3, 5, 7 };
            string[] words = { "three", "five", "seven", "ignored" };

            var zipped = numbers.Zip(words, (number, word) => number + " = " + word);

            foreach (var zip in zipped)
            {
                Console.WriteLine(zip);
            }
            Console.WriteLine();
        }

        private void ExecuteOrderBy()
        {
            var query = names
                .OrderBy(name => name.Length)
                .ThenBy(name => name);

            foreach (var element in query)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            var query2 = from name in names
                         orderby name.Length, name[0], name[1]
                         select name.ToUpper();

            foreach (var element in query2)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            var query3 = dataContext
                .Purchases
                .OrderBy(purchase => purchase.Customer.Name)
                .ThenByDescending(purchase => purchase.Price);

            foreach (var element in query3)
            {
                Console.WriteLine(element.Customer.Name + " " + element.Description + " " + element.Price);
            }
            Console.WriteLine();
        }

        private void ExecuteGrouping()
        {
            string[] files = Directory.GetFiles("E:\\Фільми");

            IEnumerable<IGrouping<string, string>> query = files
                .GroupBy(file => Path.GetExtension(file), file => file.ToUpper())
                .OrderBy(file => file.Key);

            foreach (IGrouping<string, string> element in query)
            {
                Console.WriteLine("Extension: " + element.Key);
                foreach (var subelement in element)
                {
                    Console.WriteLine("   - " + subelement);
                }
            }
            Console.WriteLine();

            var query2 = from file in files
                         group file.ToUpper() by Path.GetExtension(file) into grouping
                         orderby grouping.Key
                         select grouping;

            foreach (IGrouping<string, string> element in query2)
            {
                Console.WriteLine("Extension: " + element.Key);
                foreach (var subelement in element)
                {
                    Console.WriteLine("   - " + subelement);
                }
            }
            Console.WriteLine();

            var query3 = from p in dataContext.Purchases
                         group p.Price by p.Date.Month into salesByYear
                         select new
                         {
                             Month = salesByYear.Key,
                             TotalValue = salesByYear.Sum()
                         };

            foreach (var element in query3)
            {
                Console.WriteLine(element.Month + " " + element.TotalValue);
            }
            Console.WriteLine();
        }

        private void ExecuteSetOperators()
        {
            int[] array1 = { 1, 2, 3 };
            int[] array2 = { 3, 4, 5 };

            var concat = array1.Concat(array2);
            var union = array1.Union(array2);
            var intersect = array1.Intersect(array2);
            var except1 = array1.Except(array2);
            var except2 = array2.Except(array1);

            foreach (var element in concat)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            foreach (var element in union)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            foreach (var element in intersect)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            foreach (var element in except1)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            foreach (var element in except2)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            //MethodInfo[] methods = typeof(string).GetMethods();
            //PropertyInfo[] properties = typeof(string).GetProperties();
            //IEnumerable<MemberInfo> both = methods.Concat<MemberInfo>(properties);

            //foreach(var element in both)
            //{
            //    Console.WriteLine(element);
            //}
            //Console.WriteLine();
        }

        private void ExecuteConverting()
        {
            ArrayList classicList = new ArrayList();
            classicList.AddRange(new int[] { 3, 4, 5 });

            DateTime offender = DateTime.Now;
            classicList.Add(offender);

            IEnumerable<int> sequence1 = classicList.Cast<int>();
            IEnumerable<int> sequence2 = classicList.OfType<int>();

            foreach (var element in sequence2)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            var castLong = classicList.OfType<int>().Select(element => (long)element);
            foreach (var element in castLong)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();
        }

        private void ExecuteElementOperators()
        {
            int[] array = { 1, 2, 3, 4, 5 };

            Console.WriteLine(array.FirstOrDefault());

            Console.WriteLine(array.LastOrDefault());

            Console.WriteLine(array.FirstOrDefault(element => element % 2 == 0));

            Console.WriteLine(array.LastOrDefault(element => element % 2 == 0));

            //Console.WriteLine(array.First(element => element > 10));

            Console.WriteLine(array.FirstOrDefault(element => element > 10));

            //Console.WriteLine(array.SingleOrDefault(element => element > 3));

            Console.WriteLine(array.SingleOrDefault(element => element > 10));

            Console.WriteLine(array.ElementAt(2));

            Console.WriteLine(array.ElementAtOrDefault(9));
        }

        private void ExecuteAggregation()
        {
            int numOfDigits = "p1ssw0rd".Count(letter => char.IsDigit(letter));

            Console.WriteLine(numOfDigits);

            int[] array = { 28, 32, 14 };

            Console.WriteLine(array.Min());
            Console.WriteLine(array.Max());
            Console.WriteLine(array.Max(number => number % 10));

            var maxPrice = dataContext.Purchases.Max(purch => purch.Price);
            Console.WriteLine(dataContext.Purchases.Where(purch => purch.Price == maxPrice).FirstOrDefault().Description);

            Console.WriteLine(array.Sum(number => number % 10));
            Console.WriteLine(array.Average(number => number % 10));

            var query = dataContext
                .Customers
                .Where(element => element.Purchases.Average(p => p.Price) > 100);

            foreach (var customer in query)
            {
                Console.WriteLine(customer.Name);
            }
            Console.WriteLine();

            var sum = array.Aggregate(0, (total, n) => total + n);
            var multiplication = array.Aggregate((total, n) => total * n);
            Console.WriteLine(sum);
            Console.WriteLine(multiplication);
        }

        private void ExecuteQuantifiers()
        {
            bool hasDick = names.Contains("Dick");
            Console.WriteLine(hasDick);

            bool hasDick2 = names.Any(name => name.Length > 5);
            Console.WriteLine(hasDick2);
            Console.WriteLine();

            var query = from customer in dataContext.Customers
                        where customer.Purchases.Any(purch => purch.Price > 1000)
                        select customer.Name;
            foreach (var element in query)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            var queryLessThen1000 =
                from customer in dataContext.Customers
                where customer.Purchases.All(purch => purch.Price < 1000)
                select customer.Name;
            foreach (var customer in queryLessThen1000)
            {
                Console.WriteLine(customer);
            }
            Console.WriteLine();

            var queryWithoutPurchases =
                from customer in dataContext.Customers
                where customer.Purchases == null || customer.Purchases.Count == 0
                select customer.Name;
            foreach (var customer in queryWithoutPurchases)
            {
                Console.WriteLine(customer);
            }
            Console.WriteLine();

            foreach(var customer in dataContext.Customers)
            {
                Console.WriteLine(customer.Name);
                foreach(var purchase in customer.Purchases)
                {
                    Console.WriteLine(" ---" + purchase.Price);
                }
            }
            Console.WriteLine();
        }

        private void ExecuteGenerationMethods()
        {
            foreach(var s in Enumerable.Empty<string>())
            {
                Console.WriteLine(s);
            }

            int[][] numbers =
            {
                new int[] { 1, 2, 3},
                new int[] { 1, 3, 5},
                null
            };

            var flat = numbers.SelectMany(inner => inner ?? Enumerable.Empty<int>());
            foreach(var number in flat)
            {
                Console.WriteLine(number);
            }
            Console.WriteLine();

            foreach(var i in Enumerable.Range(5, 3))
            {
                Console.WriteLine(i + " ");
            }
            Console.WriteLine();

            foreach(var i in Enumerable.Repeat(34, 10))
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();
        }
    }
}
