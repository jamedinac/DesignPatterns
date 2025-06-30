namespace DesignPatterns.Patterns.Observer
{
    using DesignPatterns.Core;
    
    public class ObservableTask : Task
    {
        private Task task;
        private List<ITaskObserver> observers = new List<ITaskObserver>();

        public ObservableTask(Task task)
        {
            this.task = task;
        }

        public void AddObserver(ITaskObserver observer)
        {
            observers.Add(observer);
        }

        public override void Run()
        {
            task.Run();
            foreach (ITaskObserver observer in observers)
            {
                observer.OnTaskCompletion(task);
            }
        }
    }
}