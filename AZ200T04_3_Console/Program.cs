using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AZ200T04_3_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://<Your web app>.azurewebsites.net/api/echo/Sample";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("text/plain"));
            var result = client.GetStringAsync(url).GetAwaiter().GetResult();

            Console.WriteLine($"Result:\t {result}");
            Console.ReadLine();
        }
    }
}
