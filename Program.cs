using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static int num = 0;

        static void Foo(object index)
        {
            object obj = new object();
            lock (obj)
            {
                Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            }
        }


        static void ThreadsCreate(int start, int threadsNumber)
        {
            for (int i = start; i < threadsNumber; i++)
            {
                Thread thread = new Thread(Foo);
                thread.Start(i);
            }
        }

        static List<int> CreateThreadList()
        {
            List<int> numbers = new List<int>();

            for (int i = 0; i < 10000; i++)
            {
                Random random = new Random();
                numbers.Add(random.Next(0, 100000));
            }
            return numbers;
        }

        static void MinMax(ref List<int> numbers)
        {
            object obj = new object();
            lock (obj)
            {
                switch (num)
                {
                    case 0:
                        Console.WriteLine($"Minimum: {numbers.Min()}");
                        break;
                    case 1:
                        Console.WriteLine($"Maximum: {numbers.Max()}");
                        break;
                    case 2:
                        Console.WriteLine($"Average: {(int)numbers.Average()}");
                        break;
                }
                num++;
            }

        }

        static void MinMaxThread()
        {
            List<int> numbers = CreateThreadList();
            for (int i = 0; i <= 2; i++)
            {
                var thread = new Thread(() => MinMax(ref numbers));
                thread.Start();
            }
        }

        static void Main(string[] args)
        {
            //ThreadsCreate(0, 50);
            MinMaxThread();
        }
    }
}
