using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IfiNavet.Application.ApiLogic;
using IfiNavet.Application.ApiLogic.Mapper;
using IfiNavet.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IfiNavet.Application.IfiEvents.Queries.GetEventsList
{
    public class GetEventListQueryHandler : IQuery<GetEventListQuery,EventListViewModel>
    {
        private readonly IfiNavetDbContext _context;
        private readonly EventListMapper _mapper;

        public GetEventListQueryHandler(IfiNavetDbContext context)
        {
            _context = context;
            _mapper = new EventListMapper();
           
        }

        public async Task<IEnumerable<EventListViewModel>> Handle(GetEventListQuery request, CancellationToken cancellationToken)
        {
            return await _context.IfiEvents.Select(x => _mapper.Map(x)).ToListAsync(cancellationToken);
        }
    }
}