using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using IfiNavet.Infrastructure.Web;
using NUnit.Framework;

namespace IfiNavet.Infrastructure.Tests.Web
{
    [TestFixture]
    public class CookieAwareHttpClientTests
    {
        public Uri LoginURI { get; set; }
        public List<KeyValuePair<string, string>> LoginValues { get; set; } =new List<KeyValuePair<string, string>>()
        { 
            new KeyValuePair<string, string>("username", "sondrefi"),
            new KeyValuePair<string, string>("password","ifibot123"),
            new KeyValuePair<string, string>("referer", "/"), 
        };
       
        public CookieAwareHttpClient Client { get; set; }
        
        [SetUp]
        public void SetUp()
        {
            Client = new CookieAwareHttpClient();
            LoginURI = new Uri("https://ifinavet.no/login");
        }

        [Test]
        public void Should_Download_page_content()
        {
            var html = Client.GetAsync(LoginURI).Result;
            Assert.NotNull(html);
            Assert.IsFalse(string.IsNullOrWhiteSpace(html));
            Assert.IsTrue(html.Contains("html"));
            Assert.IsFalse(html.Contains("sondrefi"));
        }
        [Test]
        public void Post_Download_page_content()
        {          

            var html = Client.PostAsync(LoginURI, LoginValues).Result;
            Assert.NotNull(html);
            Assert.IsFalse(string.IsNullOrWhiteSpace(html));
            Assert.IsTrue(html.Contains("html"),"Response does not contain html");
            Assert.IsTrue(html.Contains("sondrefi"),"Response does not contain sondrefi");
        }

        [Test]
        public async Task client_should_keep_cookies()
        {
//            CookieContainer cont = new CookieContainer();
//            HttpMessageHandler handler = new HttpClientHandler(){CookieContainer = cont, UseCookies = true};
//            
//            HttpClient client = new HttpClient(handler, false);
//            var one = client.GetAsync(LoginURI).Result;
//            var two = new FormUrlEncodedContent(LoginValues);
//
//            var three =  client.PostAsync(LoginURI,two).Result.Content.ReadAsStringAsync().Result;
//            var four = client.GetAsync(LoginURI).Result.Content.ReadAsStringAsync().Result;
//            var T = cont.GetCookies(LoginURI);

            
            CancellationToken token = new CancellationToken();
            var notLoggedIn = await Client.GetAsync(LoginURI);
            Assert.IsFalse(notLoggedIn.Contains("sondrefi"),"notLoggedIn does contain sondrefi");
            var login = await Client.PostAsync(LoginURI, LoginValues,token);
            var LoggedIn =await Client.GetAsync(LoginURI);
            Assert.IsTrue(LoggedIn.Contains("sondrefi"),"LoggedIn does not contain sondrefi");
            
            Assert.AreNotSame(notLoggedIn,LoggedIn, "LoggedInn response and notLoggedIn response are same");
        }
        
    }
}