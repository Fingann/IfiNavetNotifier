using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace IfiNavetNotifier.BusinessRules
{
    public class BusinessRuleChecker
    {
        public BusinessRuleChecker()
        {
            BusinessRules = GetBussinessRules();
        }

        public IEnumerable<IBusinessRule> BusinessRules { get; }

        public IEnumerable<(string Rule, IfiEvent Event)> Enfocre(IEnumerable<IfiEvent> newEvents, IEnumerable<IfiEvent> oldEvents)
        {
            
            foreach (var newEvent in newEvents)
            {
                var oldEvent = oldEvents.FirstOrDefault(x => x.URL == newEvent.URL);
                if (oldEvent == null)
                    continue;

                foreach (var businessRule in BusinessRules)
                {
                    if (!businessRule.CheckCompliance(oldEvent, newEvent)) continue;

                    yield return (Rule: businessRule.RuleName, Event: newEvent);
                    break;
                }
            }
        }

        public IEnumerable<IBusinessRule> GetBussinessRules(params object[] constructorArgs)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IBusinessRule).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => (IBusinessRule) Activator.CreateInstance(x)).OrderByDescending(x => x.Priority);
        }
    }
}