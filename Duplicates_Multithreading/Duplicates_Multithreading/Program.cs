using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzzySharp;

namespace Duplicates_Multithreading
{
    class Program
    {
        static void Main()
        {
            List<string> names = new List<string>()
            {
                new string("Emmanuel"),
                new string("Emma"),
                new string("johnny"),
                new string("Jason"),
                new string("Jacob"),
                new string("Sean"),
                new string("David"),
                new string("Davor"),
                new string("Davidson"),
                new string("Shawn"),
                new string("Steve"),
                new string("Jameson"),
                new string("eric"),
                new string("percival"),
            };

            List<MatchedResults> results = new List<MatchedResults>();

            Parallel.ForEach(names, baseName =>
            {
                int baseNamePos = names.IndexOf(baseName);
                List<MatchedResults> comparativeValues = new List<MatchedResults>();

                Parallel.ForEach(names, comparedName =>
                {

                    int comparedNamePos = names.IndexOf(comparedName);

                    if (comparedNamePos != baseNamePos)
                    {
                        int score = Fuzz.Ratio(baseName, comparedName);
                        comparativeValues.Add(
                            new MatchedResults(baseName, comparedName, score)
                            );
                    }
                });

                var bestMatch = comparativeValues.OrderByDescending(x => x.Score).FirstOrDefault();
                results.Add(bestMatch);
            });

            var orderedResults = results.OrderByDescending(x => x.Score).ToList();

            foreach (var result in orderedResults)
            {
                Console.WriteLine($"{result.Score}: {result.BaseName} -> {result.ComparedName}");
            }

        }

        public class MatchedResults
        {
            public string BaseName { get; set; }
            public string ComparedName { get; set; }
            public int Score { get; set; }

            public MatchedResults(string baseName, string comparedName, int score)
            {
                BaseName = baseName;
                ComparedName = comparedName;
                Score = score;
            }
        }
    }
}
