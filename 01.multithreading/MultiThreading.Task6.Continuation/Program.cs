/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            var letter = Console.ReadLine();
            Task t1;
            switch (letter)
            {
                case ("a"):
                    {
                        t1 = new Task(() => Console.WriteLine("Task 1 is running"));
                        t1.ContinueWith(t =>
                        {
                            Console.WriteLine("Task 2 is running");
                        });
                        t1.Start();

                        break;
                    }
                case ("b"):
                    {
                        t1 = new Task(() => throw new NotImplementedException());
                        t1.ContinueWith(t =>
                        {
                            if (t.Status == TaskStatus.Faulted)
                            {
                                Console.WriteLine($"Task 1 failed with exception: { t.Exception.GetBaseException().Message}");
                                Console.WriteLine("Task 2 is running");
                            }
                        });
                        t1.Start();
                        break;
                    }
                case ("c"):
                    {
                        t1 = new Task(() => throw new NotImplementedException());
                        t1.ContinueWith(t =>
                        {
                            if (t.Status == TaskStatus.Faulted)
                            {
                                Console.WriteLine($"Task 1 failed with exception: { t.Exception.GetBaseException().Message}");
                                Console.WriteLine("Task 2 is running");
                            }
                        }, TaskContinuationOptions.ExecuteSynchronously);
                        t1.Start();
                        break;
                    }
                case ("d"):
                    {
                        var cts = new CancellationTokenSource();
                        CancellationToken token = cts.Token;
                        t1 = new Task(() =>
                        {
                            Task.Delay(2000);
                            Console.WriteLine("Task 1 is running");
                        }, token);
                        t1.Start();
                        try
                        {
                            cts.Cancel();
                            t1.Wait();
                        }
                        catch(Exception ex)
                        {

                        }
                        finally
                        {
                            cts.Dispose();
                        }
                        if(t1.Status == TaskStatus.Canceled)
                        {
                            t1.ContinueWith(t =>
                            {
                                Console.WriteLine($"Task 1 is cancelled");
                                Console.WriteLine("Task 2 is running");
                            }, TaskContinuationOptions.LongRunning);
                        }
                        break;
                    }
            }

            Console.ReadLine();
        }
    }
}
