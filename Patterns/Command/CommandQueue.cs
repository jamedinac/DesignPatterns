namespace DesignPatterns.Patterns.Command
{
    public class CommandQueue
    {
        private Queue<ICommand> queue = new Queue<ICommand>();
        public void Enqueue(ICommand command)
        {
            queue.Enqueue(command);
        }
        public void ExecuteAll()
        {
            while (queue.Count > 0)
            {
                queue.Dequeue().Execute();
            }
        }
    }
}