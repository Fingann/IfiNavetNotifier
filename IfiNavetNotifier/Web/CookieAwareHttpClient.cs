using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Threading;

namespace IfiNavetNotifier.Web
{
    public class CookieAwareHttpClient
    {
        private  CookieContainer CookieContainer { get; }

        private  HttpClientHandler Handler { get;  }
        private  HttpClient Client { get; }

        public CookieAwareHttpClient()
        {
            CookieContainer = new CookieContainer();
            Handler = new HttpClientHandler(){ CookieContainer = CookieContainer, UseCookies = true};
            Client = new HttpClient(Handler,false);
        }

        public async Task<string> GetAsync(Uri uri,CancellationToken token = new CancellationToken())
        {
            
                return await Client.GetStringAsync(uri);
           
        }
        public async Task<string> PostAsync(Uri uri,IEnumerable<KeyValuePair<string,string>> values,CancellationToken token = new CancellationToken())
        {
                var content = new FormUrlEncodedContent(values);
            
            //content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
               var response =await Client.PostAsync(uri, content, token);
            
               return response.Content.ReadAsStringAsync().Result;
            
        }
        
        
        
    }
}