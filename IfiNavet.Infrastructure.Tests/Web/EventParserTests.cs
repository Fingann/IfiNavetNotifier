using System;
using System.Threading.Tasks;
using IfiNavet.Core.Interfaces.Web;
using IfiNavet.Infrastructure.Web;
using NUnit.Framework;
using System.Linq;
using IfiNavet.Core.Entities;

namespace IfiNavet.Infrastructure.Tests.Web
{  
    [TestFixture]
    public class WebParserTests
    {
        public EventParser WebParser { get; set; }
        
        [SetUp]
        public void init()
        {
            WebParser = new EventParser();
        }

        [Test]
        public async Task Event_Should_return_Event_Links()
        {
            var temp = await WebParser.GetEventLinks();
            foreach (var link in temp)
            {
                Console.WriteLine(link);
            }
            Assert.IsNotEmpty(temp);

        }

        [TestCase("http://ifinavet.no/event/222", ExpectedResult = "Bedriftspresentasjon med EVRY" )]
        [TestCase("http://ifinavet.no/event/225",ExpectedResult = "Bedriftspresentasjon med Itera")]
        [TestCase("http://ifinavet.no/event/229",ExpectedResult = "Bedriftspresentasjon med Computas")]
       public string Eventid_should_return_Event(string link)
        {
            var temp = WebParser.GetEvent(new Uri(link));
            return temp.Result?.Name;

        }






    } 
    }
