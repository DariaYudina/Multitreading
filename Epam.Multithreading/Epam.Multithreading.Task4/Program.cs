using System;
using System.Threading;

namespace Epam.Multithreading.Task4
{

    class Program
    {
        static void Main(string[] args)
        {
            int number = 10;
            Thread myThread = new Thread(new ParameterizedThreadStart(CreateTread));
            myThread.Start(number);
            myThread.Join();
            Console.ReadLine();
        }
        public static void CreateTread(object number)
        {
            int n = (int)number;
            if (n > 0)
            {
                Console.WriteLine(n);
                n--;
                Thread thread = new Thread(new ParameterizedThreadStart(CreateTread));
                thread.Start(n);
                thread.Join();
                Console.WriteLine("Thread " +Thread.CurrentThread.ManagedThreadId + " finished");
            }
        }
    }
}
