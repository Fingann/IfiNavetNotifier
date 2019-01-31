using System;
using System.Collections.Generic;
using System.Linq;

namespace IfiNavetNotifier
{
    public static class IfiEventExtentions
    {


        public static IEnumerable<IfiEvent> RemovePastEvents(this IEnumerable<IfiEvent> events)
        {
            return events.Where(x => x.Date > DateTime.Now);
        }

        
    }
}
