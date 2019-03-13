using System.Net;
using System.Net.Http;

namespace IfiNavetNotifier.Web
{
    
    public static class StaticHttpClient
    {
        private static  CookieContainer CookieContainer { get; } =new CookieContainer();

        private static  HttpClientHandler Handler { get;  } = new HttpClientHandler(){ CookieContainer = CookieContainer, UseCookies = true};
        public static  HttpClient Client { get; } = new HttpClient(Handler,false);

    }
}