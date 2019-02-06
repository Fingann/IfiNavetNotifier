using System;
using System.Collections.Generic;
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
            new KeyValuePair<string, string>("password","ifibot1223"),
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
        public void client_should_keep_cookies()
        {
            var notLoggedIn = Client.GetAsync(LoginURI).Result;
            Assert.IsFalse(notLoggedIn.Contains("sondrefi"),"notLoggedIn does contain sondrefi");
            var login = Client.PostAsync(LoginURI, LoginValues).Result;
            var LoggedIn = Client.GetAsync(LoginURI).Result;
            Assert.IsTrue(LoggedIn.Contains("sondrefi"),"LoggedIn does not contain sondrefi");
            
            Assert.AreNotSame(notLoggedIn,LoggedIn, "LoggedInn response and notLoggedIn response are same");
        }
        
    }
}