using System.Threading;
using System.Threading.Tasks;
using IfiNavet.Core.Entities.Users;
using IfiNavet.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IfiNavet.Application.Users.Commands.CreateUserLogin
{
    public class CreateUserLoginCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        //Internal class 
        public class Handler :IRequestHandler<CreateUserLoginCommand, bool>
        {
            private readonly IfiNavetDbContext _context;
            private readonly IMediator _mediator;
            
            public Handler(IfiNavetDbContext context,IMediator mediator) 
            {
                _context = context;
            }
            
            public async Task<bool> Handle(CreateUserLoginCommand request, CancellationToken cancellationToken)
            {
                var entity = new UserLogin(request.UserName, request.Password);
                if (await _context.UserLogins.AnyAsync(x => x.Username == entity.Username, cancellationToken))
                {
                    return false;
                }
                await _context.UserLogins.AddAsync(entity,cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new UserLoginCreated{UserName = entity.Username}, cancellationToken);
                return true;
            }

        } 
        
    }
}