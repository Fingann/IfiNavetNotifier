using System.Collections.Generic;

namespace IfiNavetNotifier
{
    public interface IListComparer
    {
        List<IfiEvent> Compare(IEnumerable<IfiEvent> newEvents, IEnumerable<IfiEvent> oldEvents);
    }
}