using Polly;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientWeb
{
    class Program
    {

        static void Main(string[] args)
        {
            var policy = Policy.Handle<HttpRequestException>()
                .WaitAndRetry(4,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine("timeSpan:" + timeSpan);
                    Console.WriteLine("retryCount:" + retryCount);
                });

            HttpClient _client = new HttpClient();
            var baseURL = "https://localhost:5002/";

            _client.BaseAddress = new Uri(baseURL);
            _client.DefaultRequestHeaders.Accept.Clear();
            //_client.DefaultRequestHeaders.Accept.Add( );
            new MediaTypeWithQualityHeaderValue("application/json");

            try
            {
                policy.Execute(() => MakeCall(_client).GetAwaiter().GetResult());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Environment.Exit(-1);
            }



            Console.WriteLine("Hello World!");
        }

        static async Task<string> MakeCall(HttpClient _client)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(
                        @"/WeatherForecast");
                response.EnsureSuccessStatusCode();

                // return URI of the created resource.
                var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return "1";
        }

    }
}
