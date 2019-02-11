using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using IfiNavet.Application.ApiLogic;
using IfiNavet.Core.Entities.Users;
using IfiNavet.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IfiNavet.Application.UserLogins.Commands.CreateUserLogin
{
    public class CreateUserLoginCommand 
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public class Handler: IRequest<CreateUserLoginCommand>
        {
            private readonly IfiNavetDbContext _context;

            public Handler(IfiNavetDbContext context) 
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
                return true;
            }

        } 
        
    }
}