namespace DesignPatterns
{
    using DesignPatterns.Core;
    using DesignPatterns.Decorators;
    using DesignPatterns.Patterns.Adapter;
    using DesignPatterns.Patterns.Command;
    using DesignPatterns.Patterns.Composite;
    using DesignPatterns.Patterns.Observer;

    public class ImageTask : Task
    {
        public override void Run()
        {
            Console.WriteLine("This is an image task running");
        }
    }

    public class PdfTask : Task
    {
        public override void Run()
        {
            Console.WriteLine("This is a Pdf task running");
        }
    }

    public class EmailTask : Task
    {
        public override void Run()
        {
            Console.WriteLine("This is an Email task running");
        }
    }

    public class TaskFactory
    {
        public static Task CreateTask(string taskType)
        {
            switch (taskType)
            {
                case "Image":
                    return new ImageTask();
                case "Pdf":
                    return new PdfTask();
                case "Email":
                    return new EmailTask();
                default:
                    throw new NotSupportedException("task type not supported");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            List<string> tasks = new List<string> { "Email", "Image", "Pdf" };

            CommandQueue queue = new CommandQueue();
            IExecutionStrategy immediateStrategy = new ImmediateExecution();

            Task legacyTask = new LegacyJobAdapater(new LegacyPrintJob());
            Task decoratedLegacyTask = new RetryTaskDecorator(
                new TimingTaskDecorator(
                    new LoggingTaskDecorator(legacyTask)
                ), retries: 3);

            var observableTask = new ObservableTask(decoratedLegacyTask);
            observableTask.AddObserver(new ConsoleLogger());
            observableTask.AddObserver(new MetricsLogger());


            ICommand legacyCommand = new TaskCommand(observableTask, immediateStrategy);
            queue.Enqueue(legacyCommand);

            var compositeTasks = new CompositeTask();
            foreach (string taskType in tasks)
            {
                Task baseTask = TaskFactory.CreateTask(taskType);
                Task decoratedTask = new RetryTaskDecorator(
                            new TimingTaskDecorator(
                                new LoggingTaskDecorator(baseTask)
                            ),
                            retries: 3);

                observableTask = new ObservableTask(decoratedTask);
                observableTask.AddObserver(new ConsoleLogger());
                observableTask.AddObserver(new MetricsLogger());

                compositeTasks.AddTask(observableTask);
            }

            ICommand command = new TaskCommand(compositeTasks, immediateStrategy);
            queue.Enqueue(command);
            queue.ExecuteAll();
        }
    }
}