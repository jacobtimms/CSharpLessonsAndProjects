using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FuzzySharp;
using System.Diagnostics;

namespace Duplicates_Multithreading
{
    class Program
    {
        static void Main()
        {
            string filepath = "/Users/JacobTimms/REPOS/C# Lessons/Duplicates_Multithreading/Duplicates_Multithreading/bin/Debug/netcoreapp3.1/PlaceNamesUkWithC.txt"
            List<string> names = System.IO.File.ReadAllLines(filepath).ToList();

            Stopwatch timer = new Stopwatch();
            timer.Start();

            List<MatchedResults> results = new List<MatchedResults>();

            Parallel.ForEach(names, baseName =>
            {
                List<MatchedResults> comparativeValues = new List<MatchedResults>();

                foreach(string comparedName in names)
                {
                    if (baseName != comparedName)
                    {
                        double score = Fuzz.Ratio(baseName, comparedName);
                        comparativeValues.Add(
                            new MatchedResults(baseName, comparedName, score)
                            );
                    }
                };
                    var bestMatch = comparativeValues.OrderByDescending(x => x.Score).FirstOrDefault();
                    results.Add(bestMatch);
            });

            var orderedResults = results.OrderByDescending(x => x.Score).AsEnumerable();
            foreach (var result in orderedResults)
            {
                Console.WriteLine($"{result.Score}: {result.BaseName} -> {result.ComparedName}");
            }

            timer.Stop();
            Console.WriteLine(timer.Elapsed.ToString());
        }

        public class MatchedResults
        {
            public string BaseName { get; set; }
            public string ComparedName { get; set; }
            public double Score { get; set; }

            public MatchedResults(string baseName, string comparedName, double score)
            {
                BaseName = baseName;
                ComparedName = comparedName;
                Score = score;
            }
        }
    }
}
