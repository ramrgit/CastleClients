using Edison.Castle.Clients.Data.Models;
using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private string _authToken = string.Empty;

        public LockService(string authToken)
        {
            this._authToken = authToken;

        }

        public async Task<bool> GetLocksWithHttpClient()
        {
            HttpClient client = new HttpClient(new NativeMessageHandler());
            client.BaseAddress = new Uri("https://mmsdev.edprop.com/CastleAPI/");
            string resultJson = string.Empty;

            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("appliation/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _authToken);

                HttpResponseMessage  response = await client.GetAsync("api/locks");

                if(response.IsSuccessStatusCode)
                {
                    resultJson = await response.Content.ReadAsStringAsync();
                    dynamic context = JObject.Parse(resultJson);
                    IEnumerable<Lock> locks = JsonConvert.DeserializeObject<IEnumerable<Lock>>(context["Results"].ToString());
                    //Debug.WriteLine(locks.ToString());

                    return (true);

                    #region "Alternate way to Deserialize Json Arrays - override specific properties"
                    //JArray locksArray = JArray.Parse(context["Results"].ToString());
                    //IEnumerable<Lock> locks = locksArray.Select(p => new Lock
                    //{
                    //    LockId = Guid.Parse(p["LockId"].ToString()),
                    //    LockName = (string)p["LockName"],
                    //    LockUUID = Guid.Parse(p["LockUUID"].ToString()),
                    //    LockMajor = (int)p["LockMajor"],
                    //    LockMinor = (int)p["LockMinor"]
                    //}).ToList();

                    #endregion
                }
                else
                {
                    return (false);
                }
            }
            catch(Exception ex)
            {
                //Debug.WriteLine(ex.ToString());

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
