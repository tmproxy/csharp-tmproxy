using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TMProxyHelper
{
    public static class TMAPIHelper
    {
        static string url = "https://tmproxy.com/api/proxy";

        public static string Stats(string key)
        {
            var typedTemp =  new
            {
                api_key = key
            };

            string param = JsonConvert.SerializeObject(typedTemp);
            string result = PostDataJson(new HttpClient(), url + "/stats", param);
            Console.WriteLine(result);
            return result;
        }

        public static string GetCurrentProxy(string key)
        {
            var typedTemp = new
            {
                api_key = key
            };

            string param = JsonConvert.SerializeObject(typedTemp);
            string result = PostDataJson(new HttpClient(), url + "/get-current-proxy", param);
            Console.WriteLine(result);
            return result;
        }

        public static string GetNewProxy(string key, string sign, string session)
        {
            var typedTemp = new
            {
                api_key = key,
                sign = sign,
                session = session
            };

            string param = JsonConvert.SerializeObject(typedTemp);
            string result = PostDataJson(new HttpClient(), url + "/get-new-proxy", param);
            Console.WriteLine(result);
            return result;
        }

        public static string PostDataJson(HttpClient httpClient, string url, string data = null)
        {
            string html = "";
            HttpContent c = new StringContent(data, Encoding.UTF8, "application/json");
            var t = Task.Run(() => PostURI(new Uri(url), c));
            t.Wait();
            html = t.Result;

            return html;
        }

        static async Task<string> PostURI(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.PostAsync(u, c);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();

                }
            }
            return response;
        }
    }
}
