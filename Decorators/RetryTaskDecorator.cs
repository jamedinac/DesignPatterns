namespace DesignPatterns.Decorators
{
    using DesignPatterns.Core;
    
    public class RetryTaskDecorator : Task
    {
        Task task;
        int retries;

        public RetryTaskDecorator(Task task, int retries)
        {
            this.task = task;
            this.retries = retries;
        }

        public override void Run()
        {
            int retry = 0;
            while (retry < retries)
            {
                try
                {
                    task.Run();
                    break;
                }
                catch (Exception ex)
                {
                    retry++;
                    Console.WriteLine($"Execption caugth {ex.Message}. Retry count {retry}.");
                    if (retry == retries)
                    {
                        Console.WriteLine($"Max retries reached. Operation cancelled");
                    }
                }
            }
        }
    }
}