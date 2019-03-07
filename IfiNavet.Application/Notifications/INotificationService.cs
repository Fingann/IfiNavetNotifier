using System.Threading.Tasks;
using IfiNavet.Application.Notifications.Models;

namespace IfiNavet.Application.Notifications
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}