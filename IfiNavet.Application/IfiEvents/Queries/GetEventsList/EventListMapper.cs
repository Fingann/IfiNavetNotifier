using IfiNavet.Application.ApiLogic.Mapper;
using IfiNavet.Core.Entities;

namespace IfiNavet.Application.IfiEvents.Queries.GetEventsList
{
    public class EventListMapper: IMapper<IfiEvent,EventListViewModel>
    {
        public EventListViewModel Map(IfiEvent instance)
        {
            return new EventListViewModel
            {
             Name = instance.Name,
             Food = instance.Food,
             PlacesLeft =instance.PlacesLeft,
             Location =instance.Location,
             Link = instance.Link,
             Date =instance.Date,
             Open = instance.Open
            };
        }
    }
}