using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IfiNavet.Core.Entities;
using IfiNavet.Core.Entities.Users;

namespace IfiNavet.Core.Interfaces.Web
{
    public interface IEventClient
    {
        
        Task<IEnumerable<IfiEvent>> GetEvents();       
        Task<IfiEvent> GetEvent(Uri uri);
        
    }
}