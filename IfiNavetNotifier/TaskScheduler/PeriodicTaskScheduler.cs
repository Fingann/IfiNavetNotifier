using System;
using System.Threading;
using System.Threading.Tasks;

namespace IfiNavetNotifier.TaskScheduler
{
    public class PeriodicTaskScheduler: ITaskScheduler
    {
        private TimeSpan Interval { get; }
        private CancellationTokenSource CancellationTokenSource { get; set; } 
        public PeriodicTaskScheduler(TimeSpan interval)
        {
            Interval = interval;
        }
        public void Start(Func<Task> task)
        {
            CancellationTokenSource =  new CancellationTokenSource();
            PeriodicTask(task, Interval, CancellationTokenSource.Token);
        }

        public void Stop()
        {
            CancellationTokenSource.Cancel();
        }
        
        private async void PeriodicTask(Func<Task> task,TimeSpan interval, CancellationToken cancellationToken)
        {
            while (true)
            {
                await task();
                await Task.Delay(interval, cancellationToken);
            }
        }
    }
}