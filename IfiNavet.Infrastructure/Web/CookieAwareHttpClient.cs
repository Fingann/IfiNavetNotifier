using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IfiNavet.Infrastructure.Web
{
    public class CookieAwareHttpClient
    {
        private HttpClientHandler Handler { get; set; }

        public CookieAwareHttpClient()
        {
            Handler = new HttpClientHandler();
        }

        public async Task<string> GetAsync(Uri uri)
        {
            using (var client = new HttpClient(Handler))
            {
                return await client.GetStringAsync(uri);
            }
        }
        public async Task<string> PostAsync(Uri uri,IEnumerable<KeyValuePair<string,string>> values)
        {
            var content = new FormUrlEncodedContent(values);
            using (var client = new HttpClient(Handler))
            {
               var response = await client.PostAsync(uri, content);
               return await response.Content.ReadAsStringAsync();
            }
        }
        
        
        
    }
}