using ModernHttpClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Edison.Castle.Clients.Data
{
    public class AuthenticationService
    {
        private HttpClient _castleClient = new HttpClient(new NativeMessageHandler());

        public AuthenticationService()
        {
            _castleClient.BaseAddress = new Uri("https://mmsdev.edprop.com/CastleAPI/");
            
        }

        public async Task<bool> Authenticate(string login, string password)
        {
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://mmsdev.edprop.com/CastleAPI/api/locks");
                request.Method = "GET";
                request.ContentType = "application/json";
                string result = string.Empty;
                try
                {
                    
                    Task<WebResponse> webResponseTask = request.GetResponseAsync();
                    webResponseTask.Wait();
                    WebResponse webResponse = webResponseTask.Result;
                    

                    Stream webStream = webResponse.GetResponseStream();
                    StreamReader responseReader = new StreamReader(webStream);
                    result = responseReader.ReadToEnd();
                }
                catch (Exception e)
                {
                   
                }




                //var response = await _castleClient.GetAsync("api/locks");
                //if (response.IsSuccessStatusCode)
                //{

                //}

                //implement a rest client to authenticate and set the 
                Debug.WriteLine(result);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
            
       
        }
    }
}
