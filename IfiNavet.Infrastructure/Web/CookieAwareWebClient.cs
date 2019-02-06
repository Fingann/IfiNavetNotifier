using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using HtmlAgilityPack;
using IfiNavet.Core.Entities.Users;

namespace IfiNavet.Infrastructure.Web
{
    internal class CookieAwareWebClient :WebClient
    {
        //Source : https://stackoverflow.com/questions/31129578/using-cookie-aware-webclient
        
        
        public CookieContainer Cookies { get; set; }

        public CookieAwareWebClient()
        {
                ClearCookies();
            
        }
        
        
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.CookieContainer = Cookies;
            return request;
        }
        //In case you need to clear the cookies
        public CookieAwareWebClient ClearCookies() {
            Cookies = new CookieContainer();
            return this;
        }

        public CookieAwareWebClient Login(string url, UserLogin user)
        {
            //var loginUri = new Uri("https://ifinavet.no/login");
            
//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(loginUri);
//            request.Method = "POST";
//            request.ContentType = "application/x-www-form-urlencoded";
            
            var values = new NameValueCollection
            {
                { "password", user.Password },
                { "username", user.Username },
                { "referer", "/" }
            };
            Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            var responseBytes = UploadValues(url,"POST", values);
            var responsebody = System.Text.Encoding.UTF8.GetString(responseBytes);
        
            return this;
        }
    }
}