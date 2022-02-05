using System;

namespace FibonacciRecursion
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i=0; i<10; i++)
            {
                Console.Write(" | " + Fibonacci(i));
            }
        }

        static int Fibonacci(int pos)
        {
            if (pos == 0)
                return 0;

            if (pos == 1)
                return 1;

            return Fibonacci(pos - 1) + Fibonacci(pos - 2);
        }
    }
}
