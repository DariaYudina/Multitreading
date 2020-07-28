using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace Epam.Multithreading.Task6
{
    class Program
    {
        static List<string> collection = new List<string>();
        static bool collectionChanged = false;
        static object locker = new object();
        static Task task1 = new Task(WorkWithCollection);
        static Task task2 = new Task(WorkWithCollection);

        static void Main(string[] args)
        {
            task1.Start();
            task2.Start();
            Console.Read();
        }

        public static void WorkWithCollection()
        {
            for (int i = 0; i < 10; i++)
            {
                lock (locker)
                {

                    if (!collectionChanged)
                    {
                        AddingToCollection(i);
                    }
                    else
                    {
                        PrintCollection(i);
                    }
                    Thread.Sleep(1000);
                }
            }
        }

        private static void PrintCollection(int i)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
            collectionChanged = false;
            Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}; iteration: {i}");
        }

        private static void AddingToCollection(int i)
        {
            collection.Add("element");
            Console.WriteLine("element added");
            collectionChanged = true;
            Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}; iteration: {i}");
        }
    }
}
