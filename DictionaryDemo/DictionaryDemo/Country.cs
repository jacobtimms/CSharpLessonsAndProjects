using System;
using System.Collections.Generic;

namespace DictionaryDemo
{
    public class Country
    {
        public string Capital { get; set; }
        public int Population { get; set; }
        public int Size { get; set; }

        public Country(string capital, int pop, int size)
        {
            Capital = capital;
            Population = pop;
            Size = size;
        }

        public static Dictionary<String, Country> GetCountries()
        {
            var countries = new Dictionary<String, Country>();

            countries.Add("Greece", new Country("Athens", 14084, 19843));
            countries.Add("Cuba", new Country("Havana", 14084, 19843));

            return countries;
        }
    }
}
