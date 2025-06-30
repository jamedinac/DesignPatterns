namespace DesignPatterns.Patterns.Composite
{
    using DesignPatterns.Core;
    
    public class CompositeTask : Task
    {
        private List<Task> subtasks = new List<Task>();

        public void AddTask(Task task)
        {
            subtasks.Add(task);
        }

        public override void Run()
        {
            foreach (Task task in subtasks)
            {
                task.Run();
            }
        }
    }
}