using System;
using System.Data;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace JobsityChallengeAPI.Services
{
    public class Stock
    {
        public async Task<string> getStock(string code) {
            Console.WriteLine(code);
             
            string result = "";

            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://stooq.com/"); 
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.GetAsync("q/l/?s=" + code + "&f=sd2t2ohlcv&h&e=csv").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                     result = response.Content.ReadAsStringAsync().Result;
                    
                    
                }
            }
            return result;
           
        }
    }
}
