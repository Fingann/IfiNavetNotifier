using System;
using System.Collections.Generic;

namespace IfiNavetNotifier
{
    public interface IWebParser<T>
    {
        IEnumerable<T> GetEntitys();
    }
}
