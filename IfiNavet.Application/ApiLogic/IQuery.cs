using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IfiNavet.Application.IfiEvents.Queries.GetEventsList;

namespace IfiNavet.Application.ApiLogic
{
    public interface IQuery<in T,TG>
    {
        Task<IEnumerable<TG>> Handle(T request, CancellationToken cancellationToken);
    }
}