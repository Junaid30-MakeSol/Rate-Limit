using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;

namespace Github.RateLimit
{
    public class Program
    {
            public static void Main(string[] args)
            {
                Task.WaitAll(ExecuteAsync());
                Console.ReadLine();
            }

            public static async Task ExecuteAsync()
            {
                var RateLimit_Limit = "X-RateLimit-Limit";
                var RateLimit_Remaining = "X-RateLimit-Remaining";

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://api.github.com");
                var token = "<tokken>";   // enter the Github PAT

                client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);

                var response = await client.GetAsync("/search/code");

                var responseRateLimit_Limit = Convert.ToInt32(response.Headers.GetValues(RateLimit_Limit).FirstOrDefault());
                var responseRateLimit_Remaining = Convert.ToInt32(response.Headers.GetValues(RateLimit_Remaining).FirstOrDefault());

                Console.WriteLine($"RateLimit Limit {responseRateLimit_Limit}");
                Console.WriteLine($"RateLimit Remaining {responseRateLimit_Remaining}");
                var percentage = (float)responseRateLimit_Limit / 100 * 10;

                if(percentage < responseRateLimit_Remaining)
                {
                    Console.WriteLine("below 10 %");
                }
                
            }
        
    }

}