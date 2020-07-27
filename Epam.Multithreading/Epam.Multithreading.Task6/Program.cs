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

        static void Main(string[] args)
        {

            Parallel.For(0, 20, WorkWithCollection);
            Console.Read();
        }

        public static void WorkWithCollection(int x)
        {
            lock (locker)
            {
                if (collectionChanged == false)
                {
                    collection.Add("element");
                    Console.WriteLine("element added");
                    collectionChanged = true;
                    Thread.Sleep(2000);
                }
                else
                {
                    foreach (var item in collection)
                    {
                        Console.WriteLine(item);
                    }
                    collectionChanged = false;
                    Thread.Sleep(2000);
                }
            }
        }

        public static void DisplayCollection()
        {
            if(collectionChanged == true)
            {
                foreach (var item in collection)
                {
                    Console.WriteLine(item);
                }
                collectionChanged = false;
            }
        }
    }
}
