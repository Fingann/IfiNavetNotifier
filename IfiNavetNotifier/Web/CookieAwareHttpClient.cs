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
        
        
        public async Task<string> GetAsync(Uri uri,CancellationToken token = new CancellationToken())
        {
    
            var content = await StaticHttpClient.Client.GetStringAsync(uri);
            if (content.Contains("sondrefi"))
            {
                Console.WriteLine("LOGGIN");
            }
            return content;
           
        }
        public string PostAsync(Uri uri,FormUrlEncodedContent content)
        {                
               var response =StaticHttpClient.Client.PostAsync(uri, content).Result;
               var temp = response.Content.ReadAsStringAsync().Result;
               return temp;
            
        }
        
        
        
    }
}