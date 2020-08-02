using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Epam.Multithreading.Task9
{
    public class WebRequestGetExample
    {
        static void DownloadPage(string url, CancellationToken token)
        {
            Thread.Sleep(8000);
            if (token.IsCancellationRequested)
            {
                Console.WriteLine();
                Console.WriteLine("Process inerrupted");
                return;
            }
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
        static async Task DownloadPageAsync(string url, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;
            await Task.Run(() => DownloadPage(url, token));
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
            CancellationTokenSource cts = new CancellationTokenSource();
            foreach (var item in urlsArr)
            {
                tasks.Add(DownloadPageAsync(item, cts.Token));
            }
            Task task = Task.Run(() => { Task.WaitAll(tasks.ToArray()); });
            while (!task.IsCompletedSuccessfully || !cts.Token.IsCancellationRequested)
            {
                Console.WriteLine("Enter \"y\" if you want to cancel the download:");
                if(ConsoleReadLineWithTimeout() == "y")
                {
                    cts.Cancel();
                }
                Thread.Sleep(8000);
            }
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
