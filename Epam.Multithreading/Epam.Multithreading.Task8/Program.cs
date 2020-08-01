using System;
using System.Threading;
using System.Threading.Tasks;
 
namespace Epam.Multithreading.Task8
{
    class Program
    {
        static void SumOfNumbers(int n, CancellationToken token)
        {
            Console.WriteLine();
            Console.WriteLine("Started to calculate sum of " + n);
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine();
                    Console.WriteLine("Process inerrupted");
                    return;
                }
                result += i;
                Console.WriteLine();
                Console.WriteLine($"Sum of {i} is {result}");
                Thread.Sleep(1000);
            }
        }
        static async Task SumAsync(int n, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;
            await Task.Run(() => SumOfNumbers(n, token));
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter upper limit:");
            CancellationTokenSource cts = new CancellationTokenSource();
            ReadUserInput(Console.ReadLine(), out bool success, out int i);
            Task task;
            if (success)
            {
                task = SumAsync(i, cts.Token);
            }
            else
            {
                Console.WriteLine("You entered not a number");
                return;
            }

            while (!task.IsCompletedSuccessfully)
            {
                Task.Delay(5000);
                Console.WriteLine("Enter new upper limit if you want:");
                ReadUserInput(ConsoleReadLineWithTimeout(), out bool result, out int input);
                if (result)
                {
                    cts.Cancel();
                    cts = new CancellationTokenSource();
                    task = SumAsync(input, cts.Token);
                }
            }
        }

        private static void ReadUserInput(string userinput, out bool result, out int intInput)
        {
            result = int.TryParse(userinput, out intInput);
        }

        public static string ConsoleReadLineWithTimeout()
        {
            Task<string> task = Task.Factory.StartNew(Console.ReadLine);
            string result = Task.WaitAny(new Task[] { task }, TimeSpan.FromSeconds(10)) == 0
                ? task.Result
                : string.Empty;
            return result;
        }
    }
}