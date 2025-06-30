namespace DesignPatterns.Patterns.Observer
{
    using DesignPatterns.Core;
    
    public class ConsoleLogger : ITaskObserver
    {
        public void OnTaskCompletion(Task task)
        {
            Console.WriteLine($"Console log: Task {task.GetType()} completed");
        }
    }
}
