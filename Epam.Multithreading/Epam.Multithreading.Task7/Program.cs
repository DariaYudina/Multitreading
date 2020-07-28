using System;
using System.Threading;
using System.Threading.Tasks;

namespace Epam.Multithreading.Task7
{
    class Program
    {
        static void Main(string[] args)
        {
            Task7c();
            Console.ReadLine();
        }

        private static void Task7a()
        {
            //a.Continuation task should be executed regardless of the result of the parent task

            Task task1 = new Task(() =>
            {
                Console.WriteLine($"Task Id: {Task.CurrentId}");
            });

            task1.ContinueWith(Display);
            task1.Start();
        }

        private static void Task7b()
        {
            //b.Continuation task should be executed when the parent task finished without success

            Task task1 = new Task(() =>
            {
                Console.WriteLine($"Task Id: {Task.CurrentId}");
                throw new Exception("Any exeption");
            });

            task1.ContinueWith(Display, TaskContinuationOptions.OnlyOnFaulted);
            task1.Start();
        }

        private static void Task7c()
        {
            //c.Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation  

            Task task1 = new Task(() =>
            {
                Console.WriteLine($"Task Id: {Task.CurrentId}");
                Console.WriteLine(Thread.CurrentThread.Name);
                throw new Exception("Any exeption");
            });

            task1.ContinueWith(Display, TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted);
            task1.Start();
        }

        private static void Task7d()
        {
            //d.Continuation task should be executed outside of the thread pool when the parent task would be cancelled 

            Task task1 = new Task(() =>
            {
                Console.WriteLine($"Task Id: {Task.CurrentId}");
                throw new Exception("Any exeption");
            });

            task1.ContinueWith(Display, TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning);
            task1.Start();
        }

        static void Display(Task t)
        {
            Console.WriteLine($"Task Id: {Task.CurrentId}");
            Console.WriteLine($"Previous task Id: {t.Id}");
            Thread.Sleep(3000);
        }
    }
}
