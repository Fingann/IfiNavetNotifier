using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IfiNavet.Application.Events.Queries.GetEvent;
using IfiNavet.Application.Mapper;
using IfiNavet.Application.Web;
using IfiNavet.Core.Entities.Events;
using MediatR;

namespace IfiNavet.Application.Events.Queries.GetEventsList
{
    public class GetEventListQueryHandler: IRequestHandler<GetEventListQuery, IEnumerable<EventViewModel>>
    {
        private readonly IEventClient _client;
        private readonly IMapper<IfiEvent,EventViewModel> _mapper;

        public GetEventListQueryHandler(IEventClient client)
        {
            _client = client;
            _mapper = new EventListMapper();
        }
        
        public async Task<IEnumerable<EventViewModel>> Handle(GetEventListQuery request, CancellationToken cancellationToken)
        {
            var events = await _client.GetEvents();
            return events.Select(x => _mapper.Map(x));
        }
    }

    
}