using System;
using System.Collections.Generic;

namespace DictionaryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dictionary<string, string> capitals = new Dictionary<string, string>();
            //capitals.Add("England", "London");
            //capitals.Add("Norway", "Oslo");
            //capitals.Add("Canada", "Ottawa");
            //capitals.Add("Greece", "Athens");
            //capitals.Add("Cuba", "Havana");

            //string capitalOfGreece = capitals["Greece"];
            //Console.WriteLine("The capital of Greece is " + capitalOfGreece);

            var theCountries = Country.GetCountries();
            //string capitalOfCuba = theCountries["Cuba"].Capital;
            Console.WriteLine("The capital of Cuba is " + theCountries["Cuba"].Capital + ", and it's population is " + theCountries["Cuba"].Population + " People.");
        }
    }
}
