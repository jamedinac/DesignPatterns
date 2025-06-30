namespace DesignPatterns.Decorators
{
    using DesignPatterns.Core;
    
    public class LoggingTaskDecorator : Task
    {
        Task task;

        public LoggingTaskDecorator(Task task)
        {
            this.task = task;
        }

        public override void Run()
        {
            LogStart();
            task.Run();
            LogEnd();
        }

        public void LogStart()
        {
            Console.WriteLine($"Start {this.GetType()} Task");
        }

        public void LogEnd()
        {
            Console.WriteLine($"End {this.GetType()} Task");
        }

    }
}