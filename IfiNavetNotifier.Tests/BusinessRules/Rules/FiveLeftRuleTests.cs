using System;
using System.Collections.Generic;
using System.Linq;
using IfiNavetNotifier.BusinessRules;
using IfiNavetNotifier.BusinessRules.Rules;
using IfiNavetNotifier.Extentions;
using Xunit;

namespace IfiNavetNotifier.Test.BusinessRules.Rules
{
    public class FiveLeftRuleTests
    {
        public BusinessRuleChecker Checker { get; set; } = new BusinessRuleChecker();

        public IfiEvent generateEvent(int placesLeft)
        {
            return new IfiEvent()
            {
                Name = "oldEvent",
                PlacesLeft = placesLeft,
                URL = "https://ifinavet.no/event/234"
            };
        }

        [Fact]
        public void PlacesLeft_from_6_to_5_should_return_event()
        {
            var oldEvent = generateEvent(6);

            var newEvent = generateEvent(5);

            var result = Checker.Enfocre(new List<IfiEvent>() {newEvent}, new List<IfiEvent>() {oldEvent});
            Assert.Single(result);
            var tuple = result.First();
            Assert.Equal(nameof(FiveLeftRule).ToRuleName(), tuple.Item1);
            Assert.Equal(newEvent, tuple.Item2);
        }

        [Fact]
        public void PlacesLeft_old_and_new_is_five()
        {
            var oldEvent = generateEvent(5);

            var newEvent = generateEvent(5);

            var result = Checker.Enfocre(new List<IfiEvent>() {newEvent}, new List<IfiEvent>() {oldEvent});
            Assert.Empty(result);
        }

        [Fact]
        public void PlacesLeft_from_4_to_5_should_return_one_event()
        {
            var oldEvent = generateEvent(4);

            var newEvent = generateEvent(5);

            var result = Checker.Enfocre(new List<IfiEvent>() {newEvent}, new List<IfiEvent>() {oldEvent});
            Assert.Single(result);
            var tuple = result.First();
            Assert.Equal(nameof(FiveLeftRule).ToRuleName(), tuple.Item1);
            Assert.Equal(newEvent, tuple.Item2);
        }
        
        [Fact]
        public void PlacesLeft_from_null_to_5_should_return_one_event()
        {

            var newEvent = generateEvent(5);

            var result = Checker.Enfocre(new List<IfiEvent>() {newEvent}, null);
            Assert.Empty(result);
        }
        
        [Fact]
        public void PlacesLeft_from_5_to_null_should_return_one_event()
        {

            var oldEvent = generateEvent(5);

            var result = Checker.Enfocre(null, new List<IfiEvent>() {oldEvent});
            Assert.Empty(result);
        }
        
        [Fact]
        public void PlacesLeft_from_null_to_null_should_return_one_event()
        {

            var result = Checker.Enfocre(null,null);
            Assert.Empty(result);
        }
    }
}

