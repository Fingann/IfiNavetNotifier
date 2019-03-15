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

        public IEnumerable<Tuple<string, IfiEvent>> Enfocre(IEnumerable<IfiEvent> newEvents, IEnumerable<IfiEvent> oldEvents)
        {
            foreach (var newEvent in newEvents)
            {
                var oldEvent = oldEvents.FirstOrDefault(x => x.Link == newEvent.Link);
                if (oldEvent == null)
                    continue;

                foreach (var businessRule in BusinessRules)
                {
                    if (!businessRule.CheckComplience(oldEvent, newEvent)) continue;

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