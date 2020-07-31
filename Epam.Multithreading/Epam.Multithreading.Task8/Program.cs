using System;
using System.Threading;
using System.Threading.Tasks;
 
namespace Epam.Multithreading.Task8
{
    class Program
    {
        static void SumOfNumbers(int n, CancellationToken token)
        {
            Console.WriteLine("Started to calculate factorial of " + n);
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Process inerrupted");
                    return;
                }
                result += i;
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
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = SumAsync(Convert.ToInt32(Console.ReadLine()), cts.Token);

            while (!task.IsCompletedSuccessfully)
            {
                Task.Delay(5000);
                bool result;
                int input;
                ReadUserInput(ConsoleReadLineWithTimeout(), out result, out input);
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

            string result = Task.WaitAny(new Task[] { task }, TimeSpan.FromSeconds(2)) == 0
                ? task.Result
                : string.Empty;
            return result;
        }
    }
}