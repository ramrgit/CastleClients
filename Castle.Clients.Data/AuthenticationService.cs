using ModernHttpClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Portable.Text;

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
                byte[] credentialsByte = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", login, password));
                _castleClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentialsByte));

                HttpResponseMessage response = await _castleClient.GetAsync("");
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                return false;
            }
            
       
        }
    }
}
