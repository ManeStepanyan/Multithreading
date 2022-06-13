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
        static Semaphore sem = new Semaphore(1, 1);
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            ChangeStateWithThread(10);
            ChangeStateWithThreadPool(10);

            Console.ReadLine();
        }

        static void ChangeStateWithThread(int state)
        {
            state--;
            Console.WriteLine(state);

            if (state != 0)
            {
                var t = new Thread(() =>
                {
                    ChangeStateWithThread(state);
                });
                t.Start();
                t.Join();
            }
        }
        static void ChangeStateWithThreadPool(int state)
        {
            state--;
            Console.WriteLine(state);

            if (state != 0)
            {
                sem.WaitOne();
                ThreadPool.QueueUserWorkItem((a) => ChangeStateWithThreadPool(state));
                sem.Release();
            }
        }
    }
}
