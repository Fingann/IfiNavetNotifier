using IfiNavet.Application.Events.Queries.GetEvent;
using IfiNavet.Application.Mapper;
using IfiNavet.Core.Entities.Events;

namespace IfiNavet.Application.Events.Queries.GetEventsList
{
    public class EventListMapper: IMapper<IfiEvent,EventViewModel>
    {
        public EventViewModel Map(IfiEvent instance)
        {
            return new EventViewModel
            {
             Name = instance.Name,
             Food = instance.Food,
             PlacesLeft =instance.PlacesLeft,
             Location =instance.Location,
             Link = instance.Link,
             Date =instance.Date.ToString("dd/MM/yyyy HH:mm"),
             Open = instance.Open
            };
        }
    }
}