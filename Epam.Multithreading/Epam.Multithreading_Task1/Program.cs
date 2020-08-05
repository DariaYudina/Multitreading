using System;
using System.Threading.Tasks;

namespace Epam.Multithreading_Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Task[] tasks = new Task[100];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Factory.StartNew(Method);
            }

            Task.WaitAll(tasks);
            Console.WriteLine("Ending Main method");
            Console.ReadLine();

        }

        static void Method()
        {
            for (int i = 1; i <= 1000; i++)
            {
                Console.WriteLine($"Task #{Task.CurrentId} – {i}");
            }
        }
    }
}
