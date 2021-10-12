using System;
using System.Collections;

namespace LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            //LList<string> myList = new LList<string>();

            //myList.Add("1st");
            //myList.Add("2nd");
            //myList.Add("3rd");
            //myList.Add("4th");
            //myList.PrintAll();
            //Console.WriteLine();

            //myList.Add(0, "NEW 1");
            //myList.PrintAll();
            //Console.WriteLine();

            //myList.Add("4th", "NEW 2");
            //myList.PrintAll();
            //Console.WriteLine();

            //myList.Delete(4);
            //myList.PrintAll();
            //Console.WriteLine();

            //myList.Delete("3rd");
            //myList.PrintAll();
            //Console.WriteLine();

            //myList.Replace("NEW 1", "REPLACED");
            //myList.PrintAll();
            //Console.WriteLine();

            //Console.WriteLine(myList.Get(0).value);

            LList<string> bigramList = new LList<string>();
            Bigram<string> bigraminator = new Bigram<string>();
            bigramList.AddAtStart("Sean Sean");
            bigramList.AddAtStart("Sean Sean");
            bigramList.AddAtStart("Broseph");
            Console.WriteLine();
            foreach (string bro in bigramList)
            {
                Console.WriteLine(bro);
            }
            bigraminator.PrintBigrams(bigramList);
        }
    }
}
