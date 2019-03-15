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

        public IEnumerable<Tuple<string, IfiEvent>> Enforce(IEnumerable<IfiEvent> newEvents, IEnumerable<IfiEvent> oldEvents)
        {
            if (newEvents == null || oldEvents == null)
                yield break ;
            foreach (var newEvent in newEvents)
            {
                var oldEvent = oldEvents.FirstOrDefault(x => x.URL == newEvent.URL);
                if (oldEvent == null)
                    continue;

                foreach (var businessRule in BusinessRules)
                {
                    if (!businessRule.CheckCompliance(oldEvent, newEvent)) continue;
                    
                    //yield return new Tuple<string, IfiEvent>(BusinessRule: businessRule.RuleName, IfiEvent: newEvent);
                    yield return new Tuple<string, IfiEvent>(businessRule.RuleName, newEvent);
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