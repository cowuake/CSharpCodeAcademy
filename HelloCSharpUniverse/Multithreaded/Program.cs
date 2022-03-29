using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MainLibrary;

namespace Multithreaded
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ReadInput();

            var watch = new Stopwatch();
            watch.Start();

            // This will run pretty fast if no heavy computations will be done inside
            DoNotSpawnThreads(1000);

            watch.Stop();
            double noThreadsTime = watch.Elapsed.TotalMilliseconds;
            watch.Restart();

            // This will be extremely slow for any trivial computations inside, due to
            // the overhead coming from thread spawning
            SpawnSomeThreads(1000);

            watch.Stop();
            double someThreadsTime = watch.Elapsed.TotalMilliseconds;
            watch.Restart();

            // This will be faster than the previous one due to the threads being spawned
            // in parallel
            SpawnSomeCrazyThreads(1000);

            watch.Stop();
            double someCrazyThreadsTime = watch.Elapsed.TotalMilliseconds;

            Console.WriteLine();
            Console.WriteLine($"DoNotSpawnThreads\t executed in {noThreadsTime:F2} ms");
            Console.WriteLine($"SpawnSomeThreads\t executed in {someThreadsTime:F2} ms");
            Console.WriteLine($"SpawnSomeCrazyThreads\t executed in {someCrazyThreadsTime:F2} ms");
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