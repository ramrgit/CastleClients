using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CastleClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync2().Wait();
        }

        static async Task RunAsync()
        {
            //var credentials = new NetworkCredential("mmsuser", "storage2015");
            //var handler = new HttpClientHandler() { Credentials = credentials };
                
            using (var client = new HttpClient())
            {
                //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                client.BaseAddress = new Uri("https://mmsdev.edprop.com/CastleAPI/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/locks");
                if(response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.ToString());
                }

            }
        }

        static async Task RunAsync2()
        {
            using (HttpClient client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                client.BaseAddress = new Uri("https://mmsdev.edprop.com/CastleAPI/");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", @"Edison\ramr", "Good12345"))));
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "RWRpc29uXHJhbXI6R29vZDEyMzQ1");
                //Basic RWRpc29uXHJhbXI6R29vZDEyMzQ1

                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.ToString());
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized - " + response.ToString());
                }
            }
        }

    }
}
