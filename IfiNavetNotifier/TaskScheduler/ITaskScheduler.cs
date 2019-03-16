using System;
using System.Threading.Tasks;

namespace IfiNavetNotifier.TaskScheduler
{
    public interface ITaskScheduler
    {
        void Start(Func<Task> task);
        void Stop();
    }
}