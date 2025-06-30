namespace DesignPatterns.Patterns.Adapter
{
    public class LegacyPrintJob : ILegacyJob
    {
        public void ExecuteJob()
        {
            Console.WriteLine("Running legacy printing job");
        }
    }
}