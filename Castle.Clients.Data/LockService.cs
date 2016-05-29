using ModernHttpClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Edison.Castle.Clients.Data
{
    public class LockService
    {
        public LockService()
        {

        }

        public bool GetLocksWithHttpClient()
        {
            HttpClient client = new HttpClient(new NativeMessageHandler());
            client.BaseAddress = new Uri("https://mmsdev.edprop.com/CastleAPI/");
            try
            {
                Task<HttpResponseMessage>  responseTask = client.GetAsync("api/locks");
                responseTask.Wait();
                HttpResponseMessage response = responseTask.Result;

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());

            }

            return (true);


        }


        public void GetLocksWithWebRequest()
        {
        }


        public bool Unlock(Guid lockId)
        {

            //make a rest call the api to unlock this specific lock

            //the api will need the authenticated header in the rest call

            return false;

        }
    }
}
