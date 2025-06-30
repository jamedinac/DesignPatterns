namespace DesignPatterns.Core
{
    public class BatchExecution : IExecutionStrategy
    {
        private static List<Action> actions = new List<Action>();
        public void Execute(Action taskAction)
        {
            actions.Add(taskAction);
        }

        public static void Flush()
        {
            foreach (Action action in actions)
            {
                action();
            }
            actions.Clear();
        }
    }
}