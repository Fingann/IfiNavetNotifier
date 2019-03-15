using System.Net;
using System.Net.Http;

namespace IfiNavetNotifier.Web
{
    public class HttpCookieClient : HttpClient
    {
        
        public HttpCookieClient() : base(Handler)
        {
            
        }

        private static CookieContainer CookieContainer { get; } = new CookieContainer();

        private static HttpClientHandler Handler { get; } = new HttpClientHandler
            {CookieContainer = CookieContainer, UseCookies = true};
    }
}