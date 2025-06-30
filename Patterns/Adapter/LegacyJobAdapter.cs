namespace DesignPatterns.Patterns.Adapter
{
    using DesignPatterns.Core;
    
    public class LegacyJobAdapter : Task
    {
        private ILegacyJob job;
        public LegacyJobAdapter(ILegacyJob job)
        {
            this.job = job;
        }

        public override void Run()
        {
            job.ExecuteJob();
        }
    }
}