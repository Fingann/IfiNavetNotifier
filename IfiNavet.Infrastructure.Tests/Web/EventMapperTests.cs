using System;
using System.Threading.Tasks;
using IfiNavet.Core.Entities;
using IfiNavet.Core.Entities.Events;
using IfiNavet.Core.Entities.Users;
using IfiNavet.Infrastructure.Web;
using NUnit.Framework;

namespace IfiNavet.Infrastructure.Tests.Web
{
    [TestFixture]
    public class IfiEventMapperTests
    {
        
        
        [TestCase("http://ifinavet.no/event/222")]      
        public void event_html_should_give_event(string link)
        {
            var parser = new EventParser( new UserLogin("sondrefi","ifibot123"));
            Task<IfiEvent> ifiEvent = parser.GetEvent(new Uri("http://ifinavet.no/event/222"));
            Assert.AreEqual(ifiEvent.Result.Name, "Bedriftspresentasjon med EVRY");

            Assert.AreEqual("Ja",ifiEvent.Result.Food);

            Assert.AreEqual(19,ifiEvent.Result.PlacesLeft);

        }
    }
}
