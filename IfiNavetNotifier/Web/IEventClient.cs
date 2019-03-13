namespace IfiNavetNotifier.Web
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
   

   
        public interface IEventClient
        {    
            Task<IEnumerable<IfiEvent>> GetEvents();       
            Task<IfiEvent> GetEvent(Uri uri);
            bool LoggInn(UserLogin user);
        }
    }