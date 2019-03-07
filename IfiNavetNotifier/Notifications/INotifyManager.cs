﻿using System;
using System.Collections.Generic;

namespace IfiNavetNotifier.Notifications
{
    public interface INotifyManager
    {
        void Send(IEnumerable<IfiEvent> events);
    }
}