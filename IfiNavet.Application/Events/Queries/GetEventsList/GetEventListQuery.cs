using System.Collections.Generic;
using IfiNavet.Application.Events.Queries.GetEvent;
using MediatR;

namespace IfiNavet.Application.Events.Queries.GetEventsList
{
    public class GetEventListQuery: IRequest<IEnumerable<EventViewModel>>
    {
        
    }
}