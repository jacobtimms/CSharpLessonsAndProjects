using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
	public static void Main()
	{
		var people = new List<Person>()
		{
			new Person("Bill", "Smith", 41),
			new Person("Sarah", "Jones", 22),
			new Person("Stacy","Baker", 21),
			new Person("Vivianne","Dexter", 19 ),
			new Person("Bob","Smith", 49 ),
			new Person("Brett","Baker", 51 ),
			new Person("Mark","Parker", 19),
			new Person("Alice","Thompson", 18),
			new Person("Evelyn","Thompson", 58 ),
			new Person("Mort","Martin", 58),
			new Person("Eugene","deLauter", 84 ),
			new Person("Gail","Dawson", 19 ),
		};


		//1. write linq display every name ordered alphabetically
		Console.WriteLine("SOLUTION 1a");

		var sortAlphapetically = people.OrderBy(x => x.FirstName);

        Console.WriteLine("Names in alphabetical order:");
        foreach (Person person in sortAlphapetically)
        {
            Console.WriteLine(person);
        }

        //1. write linq statement for the people with last name that starts with the letter D
        //Console.WriteLine("Number of people who's last name starts with the letter D " + people1.Count());
        Console.WriteLine(Environment.NewLine + "SOLUTION 1b");

		var people1 = people.Where(x => x.LastName.ToLower().StartsWith("d"));
		Console.WriteLine("Number of people who's last name starts with the letter D: " + people1.Count());

		//2. write linq statement for all the people who are have the surname Thompson and Baker. Write all the first names to the console
		Console.WriteLine(Environment.NewLine + "SOLUTION 2");

		var ThompsonAndBakers = people.Where(x => x.LastName == "Thompson" || x.LastName == "Baker");

        Console.WriteLine("Tompson & Bakers:");
        foreach (Person person in ThompsonAndBakers)
        {
            Console.WriteLine(person.ToString());
        }

        //3. write linq to convert the list of people to a dictionary keyed by first name
        Console.WriteLine(Environment.NewLine + "SOLUTION 3");
		var peopleDict = people.ToDictionary(x => x.FirstName, x => x);

        foreach (var person in peopleDict)
        {
            Console.WriteLine(person);
        }

		// 4. Write linq statement for first Person Older Than 40 In Descending Alphabetical Order By First Name
		//Console.WriteLine("First Person Older Than 40 in Descending Order by First Name " + person2.ToString());
		Console.WriteLine(Environment.NewLine + "SOLUTION 4");

		var sortAgeThenFirstName = people.OrderBy(x => x.Age).ThenByDescending(x => x.FirstName).ToList();

		foreach (var person in sortAgeThenFirstName)
		{
			if (person.Age > 40) {
				var person2 = person;
                Console.WriteLine("First Person Older Than 40 in Descending Order by First Name: " + person2.ToString());
                break;
			}
        }
		//re-write without foreach

        //5. write a linq statement that finds all the people who are part of a family. (aka there is at least one other person with the same surname.
        Console.WriteLine(Environment.NewLine + "SOLUTION 5");

		var matchingSurnames =
			people.GroupBy(x => x.LastName);

        foreach (IGrouping<string, Person> group in matchingSurnames)
        {
			if (group.Count() > 1)
            {
				Console.WriteLine(group.Key + ":");
				foreach (Person person in group)
					Console.WriteLine(" " + person.ToString());
            }
		}

		//Also no foreach

        //6. Write a linq statement that finds which of the following numbers are multiples of 4 or 6
        Console.WriteLine(Environment.NewLine + "SOLUTION 6");

		List<int> mixedNumbers = new List<int>()
			{
				15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
			};

		IEnumerable<int> DivisibleBy4And6 =
			from num in mixedNumbers
			where num % 4 == 0 || num % 6 == 0
			select num;

		//rewrite as shorthand

		foreach (int num in DivisibleBy4And6)
		{
			Console.WriteLine(num);
		}


		// 7. How much money have we made?
		Console.WriteLine(Environment.NewLine + "SOLUTION ");

		List<double> purchases = new List<double>()
			{
				2340.29, 745.31, 21.76, 34.03, 4786.45, 879.45, 9442.85, 2454.63, 45.65
			};
		double sumOfNums = purchases.Sum();
		Console.WriteLine(sumOfNums);

	}


	public class Person
	{
		public Person(string firstName, string lastName, int age)
		{
			FirstName = firstName;
			LastName = lastName;
			Age = age;
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }

		//override ToString to return the person's FirstName LastName Age

		public override string ToString()
		{
			return ($"{FirstName} {LastName}, {Age}");
		}

	}

	
}
