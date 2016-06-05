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


        /// <summary>
        /// This method authenticates the user and returns back the basic token that needs to be stored/cached by the app on the device.
        /// The app needs to use this token with every request. 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>auth token is authentication is successful, otherwise returns string.empty</returns>
        public async Task<string> Authenticate(string login, string password)
        {
            try
            {
                byte[] credentialsByte = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", login, password));
                string base64CredString = Convert.ToBase64String(credentialsByte);
                _castleClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64CredString);

                HttpResponseMessage response = await _castleClient.GetAsync("");
                if(response.IsSuccessStatusCode)
                {
                    return base64CredString;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return string.Empty;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                throw (ex);
            }
        }


        public string GetLoggedInUser(string authToken)
        {
            try
            {
                byte[] credByte = Convert.FromBase64String(authToken);
                string decodedString = Encoding.ASCII.GetString(credByte);
                return(decodedString.Substring(0, decodedString.IndexOf(":")));
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
    }
}
