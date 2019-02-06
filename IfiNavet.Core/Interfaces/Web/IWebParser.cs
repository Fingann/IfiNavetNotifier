using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IfiNavet.Core.Entities;

namespace IfiNavet.Core.Interfaces.Web
{
    public interface IWebParser<T>
    {
        Task<IEnumerable<Uri>> GetEventLinks();
        Task<T> GetEvent(Uri uri);
    }
}
