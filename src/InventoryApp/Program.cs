using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Text;

namespace InventoryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = string.Empty;

            while (input != "Q")
            {
                Console.WriteLine("A) List categories");
                Console.WriteLine("B) List featured products");
                Console.WriteLine("C) List products in category");
                Console.WriteLine("Q) Quit");

                Console.Write("Select an option: ");

                input = Console.ReadKey().KeyChar.ToString().ToUpper();

                Console.Clear();

                switch (input)
                {
                    case "A":
                        RunAsync("api/Category/all").Wait();
                        break;
                    case "B":
                        RunAsync("api/Product/featured").Wait();
                        break;
                    case "C":
                        Console.Write("Enter category id then press <enter>: ");
                        var categoryId = Console.ReadLine();
                        RunAsync($"api/Product/category/{categoryId}").Wait();
                        break;
                }

                Console.WriteLine();
            }
        }

        static async Task RunAsync(string endpoint)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(endpoint);
                if(response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();
                    System.Console.WriteLine(responseStream.ReadAllText());
                }
                else
                {
                    System.Console.WriteLine($"An error occurred while calling the {endpoint} endpoint: {response.ReasonPhrase}");
                }
            }
        }
    }

    public static class StreamExtensions
    {
        public static string ReadAllText(this Stream stream)
        {
            var encoding = ASCIIEncoding.ASCII;
            using (var reader = new StreamReader(stream, encoding))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
