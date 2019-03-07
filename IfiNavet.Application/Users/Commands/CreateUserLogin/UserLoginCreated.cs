using System.Threading;
using System.Threading.Tasks;
using IfiNavet.Application.Notifications;
using IfiNavet.Application.Notifications.Models;
using MediatR;

namespace IfiNavet.Application.Users.Commands.CreateUserLogin
{
    public class UserLoginCreated : INotification
    {
        public string UserName { get; set; }

        public class UserLoginCreatedHandler : INotificationHandler<UserLoginCreated>
        {
            private readonly INotificationService _notification;

            public UserLoginCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(UserLoginCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new Message());
            }
        }
    }
}