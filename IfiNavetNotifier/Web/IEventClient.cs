using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IfiNavetNotifier.Web
{
    public interface IEventClient
    {
        Task<IEnumerable<IfiEvent>> GetEvents();
        Task<IfiEvent> GetEvent(Uri uri);
    }
}