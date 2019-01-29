using System;
using System.Collections.Generic;
using System.Linq;

namespace IfiNavetNotifier
{
    public class ListComparer
    {
        public List<IfiEvent> Compare(List<IfiEvent> a, List<IfiEvent> b)
        {
            List<IfiEvent> tempList = new List<IfiEvent>();
            foreach (var item in a)
            {
                var bItem = b.FirstOrDefault(x => x.ID == item.ID);
                if (bItem != null)
                {
                    if (bItem.PlacesLeft != item.PlacesLeft)
                    {
                        tempList.Add(item);
                    }
                }
            }


            return tempList; 
        }

           
    }
}
