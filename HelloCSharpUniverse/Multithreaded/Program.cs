using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MainLibrary;

namespace Multithreaded
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ReadInput();

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            DoNotSpawnThreads(1000);

            watch.Stop();
            var noThreadsTime = watch.Elapsed.TotalMilliseconds;
            watch.Restart();

            SpawnSomeThreads(1000);

            watch.Stop();
            var someThreadsTime = watch.Elapsed.TotalMilliseconds;
            watch.Restart();

            SpawnSomeCrazyThreads(1000);

            watch.Stop();
            var someCrazyThreadsTime = watch.Elapsed.TotalMilliseconds;

            Console.WriteLine();
            Console.WriteLine($"DoNotSpawnThreads executed in {noThreadsTime:F2} ms");
            Console.WriteLine($"SpawnSomeThreads executed in {someThreadsTime:F2} ms");
            Console.WriteLine($"SpawnSomeCrazyThreads executed in {someCrazyThreadsTime:F2} ms");
            Console.WriteLine();

            CLIUtils.WaitForExit();
        }

        public static string ReadInput()
        {
            Console.Write("Insert some input: ");
            return Console.ReadLine();
        }

        public static void DoNotSpawnThreads(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Sorry, not threads for you this time");
            }
        }

        public static void SpawnSomeCrazyThreads(int n)
        {
            Parallel.For(0, n, (i, state) =>
            {
                var temp = i + 1;

                Thread thread = new Thread(() => Console.WriteLine(
                    $"Spaghetti, from thread {Thread.CurrentThread.ManagedThreadId} " +
                    $"running on processor {Thread.GetCurrentProcessorId()}, " +
                    $"my real number is {temp}."));
                thread.Start();
            });
        }

        public static void SpawnSomeThreads(int n)
        {
            for (int i = 0; i < n; i++)
            {
                var temp = i + 1;

                Thread thread = new Thread(() => Console.WriteLine(
                    $"Hello world again, from thread {Thread.CurrentThread.ManagedThreadId} " +
                    $"running on processor {Thread.GetCurrentProcessorId()}, " +
                    $"my real number is {temp}."));
                thread.Start();
            }
        }
    }
}
