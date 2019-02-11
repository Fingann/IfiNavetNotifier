using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using IfiNavet.Application.ApiLogic;
using IfiNavet.Application.UserLogins.Commands.CreateUserLogin;
using IfiNavet.Core.Entities.Users;
using IfiNavet.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IfiNavet.Application.UserLogins.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public class Handler: IRequest<UpdateUserLoginCommand>
        {
            private readonly IfiNavetDbContext _context;

            public Handler(IfiNavetDbContext context) 
            {
                _context = context;
            }

            public async Task<bool> Handle(UpdateUserLoginCommand request, CancellationToken cancellationToken)
            {
                
                if (!await _context.UserLogins.AnyAsync(x => x.Username ==request.UserName,cancellationToken))
                {
                    return false;
                }
                var entity = new UserLogin(request.UserName, request.Password);
                await _context.UserLogins.AddAsync(entity,cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }

        } 
    }
}