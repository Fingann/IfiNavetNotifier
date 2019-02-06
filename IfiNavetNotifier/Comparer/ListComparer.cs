﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace IfiNavetNotifier
{
    public class ListComparer 
    {
        public List<IfiEvent> Compare(IEnumerable<IfiEvent> a, IEnumerable<IfiEvent> b)
        {
            if (a == null || b == null)
            {
                return new List<IfiEvent>();
            }
            List<IfiEvent> tempList = new List<IfiEvent>();
            foreach (var item in a)
            {
                var bItem = b.FirstOrDefault(x => x.Link == item.Link);
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
