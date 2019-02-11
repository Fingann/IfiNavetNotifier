﻿using System;
using System.Collections.Generic;
using IfiNavet.Core.Interfaces.Entities;

namespace IfiNavet.Core.Entities
{
    public class IfiEvent : EntityBase<int>
    {
        public string Name { get; set; }
        public string Food { get; set; }
        public int PlacesLeft { get; set; }
        public string Location { get; set; }
        public Uri Link { get; set; }
        public DateTime Date { get; set; }
        public bool Open { get; set; }
        
    }
}
