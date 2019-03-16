using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace IfiNavetNotifier.Web
{
    public class HttpCookieClient : HttpClient
    {
        
        public HttpCookieClient() : base(Handler)
        {
            
        }

        public Task<HttpResponseMessage> Login(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            
            var content = new FormUrlEncodedContent(parameters);

            return PostAsync("https://ifinavet.no/login", content);
        }

        private static CookieContainer CookieContainer { get; } = new CookieContainer();

        private static HttpClientHandler Handler { get; } = new HttpClientHandler
            {CookieContainer = CookieContainer, UseCookies = true};
    }
}