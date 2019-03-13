namespace IfiNavetNotifier.Web
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
   

   
        public interface IEventClient
        {    
            Task<IEnumerable<IfiEvent>> GetEvents();       
            Task<IfiEvent> GetEvent(Uri uri);
            Task<bool> LoggInn(UserLogin user);
        }
    }