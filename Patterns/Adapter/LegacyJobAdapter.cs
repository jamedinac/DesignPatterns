namespace DesignPatterns.Patterns.Adapter
{
    using DesignPatterns.Core;
    
    public class LegacyJobAdapater : Task
    {
        private ILegacyJob job;
        public LegacyJobAdapater(ILegacyJob job)
        {
            this.job = job;
        }

        public override void Run()
        {
            job.ExecuteJob();
        }
    }
}