using System.Threading.Tasks;
using IfiNavet.Application.Notifications;
using IfiNavet.Application.Notifications.Models;

namespace IfiNavet.Infrastructure.Notifications
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}