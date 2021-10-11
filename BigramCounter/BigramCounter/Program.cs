using System;
using System.Collections.Generic;
using System.Linq;

namespace BigramCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            List<String> bigrams = new List<String>();

            string input = "see the sea";
            string sentence = input.Replace(" ", "_");

            for (int i=0; i<sentence.Length-1; i++)
            {
                string output = "";
                output += sentence[i];
                output += sentence[(i + 1)];
                bigrams.Add(output);
            }

            var matchingBigrams =
                bigrams.GroupBy(x => x);

            foreach(var group in matchingBigrams)
            {
                Console.WriteLine($"{group.Key}:{group.Count()}");
            }
        }
    }
}