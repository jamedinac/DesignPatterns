namespace DesignPatterns.Decorators
{
    using DesignPatterns.Core;
    using System.Diagnostics;
    
    public class TimingTaskDecorator : Task
    {
        Task task;
        Stopwatch stopwatch = new Stopwatch();

        public TimingTaskDecorator(Task task)
        {
            this.task = task;
        }

        public override void Run()
        {
            stopwatch.Start();
            task.Run();
            stopwatch.Stop();

            Console.WriteLine($"It took {stopwatch.Elapsed}");
        }
    }
}