using System.Linq;
using IfiNavet.Core.Entities.Users;
using IfiNavet.Infrastructure.Web;
using NUnit.Framework;

namespace IfiNavet.Infrastructure.Tests.Web
{
    [TestFixture]
    public class EventWebClientTests
    {
        public EventWebClient Client { get; set; }

        [SetUp]
        public void SetUp()
        {
            Client = new EventWebClient(new UserLogin("sondrefi","ifibot123"));
        }

        [Test]
        public void Get_Events_should_Return_list()
        {
            var events = Client.GetEvents().Result;
            Assert.IsNotEmpty(events);
        }
    }
}