namespace DesignPatterns.Patterns.Observer
{
    using DesignPatterns.Core;
        
    public interface ITaskObserver
    {
        void OnTaskCompletion(Task task);
    }
}