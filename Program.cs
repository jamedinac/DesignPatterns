namespace DesignPatterns
{
    using DesignPatterns.Core;
    using DesignPatterns.Patterns.Adapter;
    using DesignPatterns.Patterns.Command;
    using DesignPatterns.Patterns.Composite;
    using DesignPatterns.Patterns.Creational;

    class Program
    {
        static void Main()
        {
            List<string> tasks = new List<string> { "Email", "Image", "Pdf" };

            var queue = new CommandQueue();
            var immediateStrategy = new ImmediateExecution();

            queue.Enqueue(CommandFactory.Create(new LegacyJobAdapter(new LegacyPrintJob()), immediateStrategy));


            foreach (string taskType in tasks)
            {
                queue.Enqueue(CommandFactory.Create(taskType, immediateStrategy));
            }

            var compositeTasks = new CompositeTask();
            foreach (string task in tasks)
            {
                compositeTasks.AddTask(TaskFactory.CreateTask(task));
            }

            queue.Enqueue(CommandFactory.Create(compositeTasks, immediateStrategy));

            queue.ExecuteAll();
        }
    }
}