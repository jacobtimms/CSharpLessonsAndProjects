using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedList
{
    class Bigram<T>
    {
        public void PrintBigrams(LList<T> firstElement)
        {
            var bigramDict = GetBigramDict(firstElement);
            foreach (KeyValuePair<string, int> bigram in bigramDict)
            {
                Console.WriteLine(bigram.Key + " - " + bigram.Value);
            }
        }
        public Dictionary<string, int> GetBigramDict(LList<T> firstElement)
        {
            Dictionary<string, int> bigramDictionary = new Dictionary<string, int>();
            var currentElement = firstElement._firstElement;
            while (currentElement != null)
            {
                for (int x = 0; x < currentElement.value.ToString().Length - 1; x++)
                {
                    var currentBigramCheck = currentElement.value.ToString().Substring(x, 2);
                    currentBigramCheck = currentBigramCheck.ToLower();
                    currentBigramCheck = currentBigramCheck.Replace(" ", "_");
                    if (bigramDictionary.ContainsKey(currentBigramCheck))
                    {
                        bigramDictionary.TryGetValue(currentBigramCheck, out int bigramCount);
                        bigramDictionary[currentBigramCheck] = bigramCount + 1;
                    }
                    else
                    {
                        bigramDictionary.Add(currentBigramCheck, 1);
                    }
                }
                currentElement = currentElement.next;
            }
            return bigramDictionary;
        }
    }
}
