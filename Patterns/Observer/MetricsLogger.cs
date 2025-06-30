namespace DesignPatterns.Patterns.Observer
{
    using DesignPatterns.Core;
    
    public class MetricsLogger : ITaskObserver
    {
        public void OnTaskCompletion(Task task)
        {
            Console.WriteLine($"Metrics log: Task {task.GetType()} completed");
        }
    }
}