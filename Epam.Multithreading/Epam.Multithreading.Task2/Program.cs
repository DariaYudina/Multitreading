using System;
using System.Threading.Tasks;

namespace Epam.Multithreading.Task2
{
    class Program
    {
        static int[] arr;
        static void Main(string[] args)
        {
            arr = new int[10];
            Task task1 = new Task(() => {
                Random random = new Random();
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = random.Next(1, 100);
                }
                Console.WriteLine("Array random values:");
                foreach (var item in arr)
                {
                    Console.WriteLine(item);
                }
            });
            task1.Start();

            Task task2 = task1.ContinueWith((Task t) =>
            {
                Random random = new Random();
                var randomNumber = random.Next(1, 100);
                Console.WriteLine($"Random number:{randomNumber}");

                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] *= randomNumber;
                }
                Console.WriteLine("Multiplies array with random integer:");

                foreach (var item in arr)
                {
                    Console.WriteLine(item);
                }
            });

            Task task3 = task2.ContinueWith((Task t) =>
            {
                    int temp;
                    for (int i = 0; i < arr.Length - 1; i++)
                    {
                        for (int j = i + 1; j < arr.Length; j++)
                        {
                            if (arr[i] > arr[j])
                            {
                                temp = arr[i];
                                arr[i] = arr[j];
                                arr[j] = temp;
                            }
                        }
                    }
                Console.WriteLine("Sorting array by ascending:");

                foreach (var item in arr)
                {
                    Console.WriteLine(item);
                }
            });

            Task task4 = task3.ContinueWith((Task t) =>
            {
                int summ = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    summ += arr[i];
                }
                Console.WriteLine("Average value: ");
                Console.WriteLine(summ/arr.Length);
            });

            Console.ReadLine();
        }
    }
}
