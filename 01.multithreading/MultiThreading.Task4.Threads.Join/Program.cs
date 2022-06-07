/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            RecursionWithThreads(10, 20);

            Console.ReadLine();
        }

        static void RecursionWithThreads(int n, int state)
        {
            if (n != 0)
            {
                var t = new Thread(() =>
                {
                    state = ChangeState(state);
                });
                t.Start();
                t.Join();
                RecursionWithThreads(--n, state);
            }
        }
        static void RecursionWithThreadPool(int n, int state)
        { //????????????
            if (n != 0)
            {
            }
        }


        static int ChangeState(int state)
        {
            state--;
            Console.WriteLine(state);
            return state;
        }
    }
}
