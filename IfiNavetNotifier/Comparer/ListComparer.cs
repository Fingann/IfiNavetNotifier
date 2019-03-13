using System.Collections.Generic;
using System.Linq;

namespace IfiNavetNotifier
{
    public class ListComparer : IListComparer
    {
        public List<IfiEvent> Compare(IEnumerable<IfiEvent> newEvents, IEnumerable<IfiEvent> oldEvents)
        {
            if (newEvents == null || oldEvents == null) return new List<IfiEvent>();

            var tempList = new List<IfiEvent>();

            foreach (var item in newEvents)
            {
                var oldItem = oldEvents.FirstOrDefault(x => x.Link == item.Link);
                if (oldItem == null) continue;

                if (oldItem.PlacesLeft == 0 && item.PlacesLeft > 0)
                    //if (oldItem.PlacesLeft != item.PlacesLeft )

                    tempList.Add(item);
            }

            return tempList;
        }
    }
}