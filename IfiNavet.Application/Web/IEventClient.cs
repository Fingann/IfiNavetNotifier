using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IfiNavet.Core.Entities;
using IfiNavet.Core.Entities.Events;
using IfiNavet.Core.Entities.Users;

namespace IfiNavet.Application.Web
{
    public interface IEventClient
    {
        
        Task<IEnumerable<IfiEvent>> GetEvents();       
        Task<IfiEvent> GetEvent(Uri uri);
        Task<bool> LoggInn(UserLogin user);
    }
}