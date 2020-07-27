using System;
using System.Threading;

namespace Epam.Multithreading.Task5
{
    class Program
    {
        static Semaphore sem = new Semaphore(1, 1);

        static void Main()
        {
            int number = 10;
            ThreadPool.QueueUserWorkItem(JobForAThread, number);
            Thread.Sleep(3000);

            Console.ReadLine();
        }

        static void JobForAThread(object number)
        {
            sem.WaitOne();
            int n = (int)number;
            if (n > 0)
            {
                Console.WriteLine(n);
                n--;
                Console.WriteLine("выполнение внутри потока из пула, thread Id: " + Thread.CurrentThread.ManagedThreadId);
                ThreadPool.QueueUserWorkItem(JobForAThread, n);
                Thread.Sleep(50);
            }
            sem.Release();
        }

    }
}
