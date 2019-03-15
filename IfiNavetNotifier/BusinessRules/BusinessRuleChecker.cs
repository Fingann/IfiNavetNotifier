using System;
using System.Collections.Generic;
using System.Linq;

namespace IfiNavetNotifier.BusinessRules
{
    public class BusinessRuleChecker
    {
        public BusinessRuleChecker()
        {
            BusinessRules = GetBussinessRules();
        }

        public IEnumerable<IBusinessRule> BusinessRules { get; set; }

        public IEnumerable<Tuple<string, IfiEvent>> Enfocre(IEnumerable<IfiEvent> newEvents, List<IfiEvent> oldEvents)
        {
            //TODO: Create return yield
            var complientEvents = new List<Tuple<string, IfiEvent>>();
            foreach (var newEvent in newEvents)
            {
                var oldEvent = oldEvents.FirstOrDefault(x => x.Link == newEvent.Link);
                if (oldEvent == null)
                    continue;

                foreach (var businessRule in BusinessRules)
                {
                    if (!businessRule.CheckComplience(oldEvent, newEvent)) continue;

                    complientEvents.Add(new Tuple<string, IfiEvent>(businessRule.RuleName, newEvent));
                    break;
                }
            }

            return complientEvents;
        }

        public IEnumerable<IBusinessRule> GetBussinessRules(params object[] constructorArgs)
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IBusinessRule).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => (IBusinessRule) Activator.CreateInstance(x)).OrderByDescending(x => x.Priority);
        }
    }
}