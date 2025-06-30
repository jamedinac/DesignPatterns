namespace DesignPatterns.Patterns.Command
{
    using DesignPatterns.Core;
    
    public class TaskCommand : ICommand
    {
        private Task task;
        private IExecutionStrategy strategy;

        public TaskCommand(Task task, IExecutionStrategy strategy)
        {
            this.task = task;
            this.strategy = strategy;
        }
        public void Execute()
        {
            strategy.Execute(() => task.Run());
        }
    }
}