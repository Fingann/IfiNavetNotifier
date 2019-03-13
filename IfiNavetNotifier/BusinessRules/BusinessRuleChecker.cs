using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Reflection;
using Org.BouncyCastle.Asn1.X9;

namespace IfiNavetNotifier.BusinessRules
{
    public class BusinessRuleChecker
    {
        public IEnumerable<IBusinessRule> BusinessRules { get; set; }
        public BusinessRuleChecker()
        {
            BusinessRules = GetBussinessRules();
        }
        public IEnumerable<Tuple<string,IfiEvent>> Enfocre (IList<IfiEvent> newEvents, IList<IfiEvent> oldEvents)
        {
            var complientEvents = new List<Tuple<string,IfiEvent>>();
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
                .Select(x => (IBusinessRule)Activator.CreateInstance(x));
        }
    }
}