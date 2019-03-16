using System;
using System.Collections.Generic;

namespace IfiNavetNotifier.Notifications
{
    public interface INotifyManager
    {
        void Send(IEnumerable<(string Rule, IfiEvent Event)> events);
        void Send((string Rule, IfiEvent Event) events);
    }
}