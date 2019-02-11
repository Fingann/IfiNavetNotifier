using System.Threading;
using System.Threading.Tasks;

namespace IfiNavet.Application.ApiLogic
{
    public interface IRequest<T>
    {
        Task<bool> Handle(T request, CancellationToken token);
    }
}