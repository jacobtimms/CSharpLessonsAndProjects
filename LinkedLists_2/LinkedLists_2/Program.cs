using System;
using System.Text;
using System.Collections.Generic;


namespace LinkedLists_2
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                // Creating a linkedlist
                // Using LinkedList class
                LinkedList<int> my_list = new LinkedList<int>();

                // Adding elements in the LinkedList
                // Using AddLast() method
                my_list.AddLast(10);
                my_list.AddLast(3);
                my_list.AddLast(9);
                my_list.AddLast(2);
                //returns 10, 3, 9, 2

                my_list.AddLast(3);
                my_list.Remove(3);
                //returns 10, 9, 2, 3

                LinkedListNode<int> current = my_list.Find(9);
                my_list.AddBefore(current, 4);
                //returns 10, 4, 9, 2, 3

                var node = my_list.First;
                while (node != null)
                {
                    var next = node.Next;
                    if (node.Value % 2 == 0)
                        my_list.Remove(node);
                    node = next;
                }
                //returns 9, 3


                // Accessing the elements of 
                // LinkedList Using foreach loop
                foreach (int num in my_list)
                {
                    Console.WriteLine(num);
                }
            }
        }
    }
}