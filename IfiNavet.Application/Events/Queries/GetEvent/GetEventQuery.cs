using MediatR;

namespace IfiNavet.Application.Events.Queries.GetEvent
{
    public class GetEventQuery : IRequest<EventViewModel>
    {
        public GetEventQuery(string link)
        {
            Link = link;
        }
        public string Link { get;}
    }
}