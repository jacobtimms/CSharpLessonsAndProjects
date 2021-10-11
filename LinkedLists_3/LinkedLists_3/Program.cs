using System;

namespace LinkedLists_3
{
    class Program
    {
        static void Main(string[] args)
        {
            //MyList<string> stringList = new MyList<string>();
            //stringList.Add("String 5");
            //stringList.Add("String 4");
            //stringList.Add("String 3");
            //stringList.Add("String 2");
            //stringList.Add("String 1");

            //stringList.AddAtPosition(1, "newString");
            //stringList.AddAfterData("String 1", "newString");
            //stringList.Delete(2);
            //stringList.Delete("String 1");
            //stringList.Get(2);
            //stringList.Replace(0, "String new");
            //stringList.Print();



            MyList<int> intList = new MyList<int>();
            intList.Add(5);
            intList.Add(4);
            intList.Add(3);
            intList.Add(2);
            intList.Add(1);

            //intList.AddAtPosition(1, 100);
            //intList.AddAfterData(1, 100);
            //intList.Delete(2);
            //intList.Delete(4);
            //intList.Get(2);
            //intList.Replace(0, 100);
            intList.Print();
        }
    }
}
