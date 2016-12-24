using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Numerics;
using System.Diagnostics;
using System.Reflection;
using Education.Classes;
using System.Collections.ObjectModel;

namespace Education
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.DoEverything();

            Console.ReadLine();
        }

        private void DoEverything()
        {
            Dictionary<Customer, int> customerImportance = new Dictionary<Customer, int>(new CustomerEqualityComparer());

            Customer petro = new Customer("Petro", "Petrenko");
            Customer petro2 = new Customer("Petro", "Petrenko");
            Customer ivan = new Customer("Ivan", "Ivanenko");

            Console.WriteLine(petro == petro2);

            customerImportance[petro] = 1;
            customerImportance[ivan] = 1;
            customerImportance[petro2] = 2;

            foreach(var customer in customerImportance.Keys)
            {
                Console.WriteLine(customer.FirstName + " " + customer.LastName + ": " + customerImportance[customer]);
            }
        }
    }

    public class Customer
    {
        public string FirstName;
        public string LastName;

        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }

    public class CustomerEqualityComparer : EqualityComparer<Customer>
    {
        public override bool Equals(Customer x, Customer y)
        {
            if (x.FirstName == y.FirstName &&
                x.LastName == y.LastName)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode(Customer obj)
        {
            return (obj.FirstName + ";" + obj.LastName).GetHashCode();
        }
    }
}