using Hangfire;

namespace Tributech.Dataspace.Workflow.Common
{
    public interface Job {

        string CronInterval { get; }
        void StartJob();
    }

    public abstract class MinutelyJob : Job
    {
        public string CronInterval => Cron.Minutely();
        public abstract void StartJob();
    }
}
