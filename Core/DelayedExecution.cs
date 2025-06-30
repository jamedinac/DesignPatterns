namespace DesignPatterns.Core
{
    public class DelayedExecution : IExecutionStrategy
    {
        public void Execute(Action taskAction)
        {
            Thread.Sleep(200);
            taskAction();
        }
    }
}