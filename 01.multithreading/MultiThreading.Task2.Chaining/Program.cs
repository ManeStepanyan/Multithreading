/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random();

            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            var task1 = new Task<int[]>(() =>
            {
                var arr = new int[10];
                for (int i = 0; i < 10; i++)
                {
                    arr[i] = rnd.Next(100);
                }

                Console.WriteLine("Task1: array");
                Array.ForEach(arr, Console.WriteLine);
                return arr;
            });

            var task2 = task1.ContinueWith(res =>
            {
                var arr = res.Result;
                var rndValue = rnd.Next(10);
                for (var i = 0; i < arr.Length; i++)
                {
                    arr[i] *= rndValue;
                }

                Console.WriteLine($"Task2: array multiplied by {rndValue}");
                Array.ForEach(arr, Console.WriteLine);
                return arr;
            });

            var task3 = task2.ContinueWith(arr =>
            {
                Array.Sort(arr.Result);
                Console.WriteLine("Task3: sorted array");
                Array.ForEach(arr.Result, Console.WriteLine);
                return arr.Result;
            });

            var task4 = task3.ContinueWith(arr =>
            {
                int sum = 0;
                foreach (var item in arr.Result)
                {
                    sum += item;
                }

                var avg = sum / arr.Result.Length;
                Console.WriteLine("Task4 ---- average value");
                Console.WriteLine(avg);
                return avg;
            });

            task1.Start();

            Console.ReadLine();
        }

    }
}
