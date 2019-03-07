using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IfiNavet.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IfiNavet.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordCommand: IRequest<bool>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public class Handler: IRequestHandler<ChangePasswordCommand, bool>
        {
            private readonly IfiNavetDbContext _context;

            public Handler(IfiNavetDbContext context) 
            {
                _context = context;
            }

            public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
            {
                var dbEntity = await _context.UserLogins.Where(x => x.Username == request.UserName).ToListAsync(cancellationToken);
                if (!dbEntity.Any())
                {
                    return false;
                }

                dbEntity.First().Password = request.Password;

                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }

        } 
    }
}