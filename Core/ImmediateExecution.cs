namespace DesignPatterns.Core
{
    public class ImmediateExecution : IExecutionStrategy
    {
        public void Execute(Action taskAction)
        {
            taskAction();
        }
    }
}