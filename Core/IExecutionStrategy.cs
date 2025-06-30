namespace DesignPatterns.Core
{
    public interface IExecutionStrategy
    {
        void Execute(Action taskAction);
    }
}