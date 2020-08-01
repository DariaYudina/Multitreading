using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Epam.Multithreading.Task9
{
    public class WebRequestGetExample
    {
        static void DownloadPage(string url)
        {
            Thread.Sleep(2000);
            Console.WriteLine("start " + url);
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;

            WebResponse response = request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer.Substring(0, 100));
            }

            response.Close();
            Thread.Sleep(1000);
            Console.WriteLine("stop " + url);
        }
        static async Task DownloadPageAsync(string url)
        {
            await Task.Run(() => DownloadPage(url));
        }

        static void Main(string[] args)
        {
            bool exit = false;
            List<string> urls = new List<string>();
            while (!exit)
            {
                Console.WriteLine("Enter url or any key for exit: ");
                string input = Console.ReadLine();
                if (Regex.IsMatch(input, @"^(?:http(s)?:\/\/)[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$"))
                {
                    urls.Add(input);
                }
                else
                {
                    exit = true;
                }
            }

            string[] urlsArr = urls.ToArray();
            List<Task> tasks = new List<Task>();
            foreach (var item in urlsArr)
            {
                tasks.Add(DownloadPageAsync(item));
            }
            CancellationTokenSource cts = new CancellationTokenSource();
            Task task = Task.Run(() => Task.WaitAll(tasks.ToArray(), cts.Token));
            while (!task.IsCompletedSuccessfully)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Enter any key if you wantwant to cancel the download");
                cts.Cancel();
                Thread.Sleep(3000);
                break;
            }

            //Task task1 = DownloadPageAsync("https://docs.microsoft.com");
            //Task task3 = DownloadPageAsync("https://yandex.ru");
            //Task.WaitAll(task1, task3);
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
