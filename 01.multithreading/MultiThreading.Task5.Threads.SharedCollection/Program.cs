/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        private static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        private static object sync = new object();
        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            var sharedCollection = new List<int>();
            var t1 = new Task(() =>
            {
                AddElements(sharedCollection);
            });
            var t2 = new Task(() =>
            {
                PrintElements(sharedCollection);
            });

            t1.Start();
            t2.Start();
            Task.WhenAll(t1, t2).Wait();

            Console.ReadLine();
        }

        static void AddElements(List<int> list)
        {
            for (int i = 0; i < 10; i++)
            {
                lock (sync)
                {
                    list.Add(i);
                }
                autoResetEvent.Set();
                Task.Delay(1000).Wait();
            }
        }

        static void PrintElements(List<int> list)
        {
            for (int j = 0; j < 10; j++)
            {
                autoResetEvent.WaitOne();
                lock (sync)
                {
                    Console.WriteLine();
                    for (int i = 0; i < list.Count; i++)
                    {
                        Console.Write($"{list[i]} ");
                    }
                }
            }
        }
    }
}
