using System;
using System.Threading;
using System.Threading.Tasks;
using IfiNavet.Application.Events.Queries.GetEventsList;
using IfiNavet.Application.Mapper;
using IfiNavet.Application.Web;
using IfiNavet.Core.Entities.Events;
using MediatR;

namespace IfiNavet.Application.Events.Queries.GetEvent
{
    public class GetEventQueryHandler: IRequestHandler<GetEventQuery, EventViewModel>
    {
        private readonly IEventClient _client;
        private readonly IMapper<IfiEvent,EventViewModel> _mapper;

        public GetEventQueryHandler(IEventClient client)
        {
            _client = client;
            _mapper = new EventListMapper();
        }
        
        public async Task<EventViewModel> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            var events = await _client.GetEvent(new Uri(request.Link));
            return _mapper.Map(events);
        }
        
    }
}