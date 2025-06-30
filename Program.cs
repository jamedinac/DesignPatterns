using System.Diagnostics;
using System.Runtime.CompilerServices;

public abstract class Task
{
    public abstract void Run();
}

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

public interface IExecutionStrategy
{
    void Execute(Action taskAction);
}

public class ImmediateExecution : IExecutionStrategy
{
    public void Execute(Action taskAction)
    {
        taskAction();
    }
}

public class DelayedExecution : IExecutionStrategy
{
    public void Execute(Action taskAction)
    {
        Thread.Sleep(200);
        taskAction();
    }
}

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

public class LoggingTaskDecorator : Task
{
    Task task;

    public LoggingTaskDecorator(Task task)
    {
        this.task = task;
    }

    public override void Run()
    {
        LogStart();
        task.Run();
        LogEnd();
    }

    public void LogStart()
    {
        Console.WriteLine($"Start {this.GetType()} Task");
    }

    public void LogEnd()
    {
        Console.WriteLine($"End {this.GetType()} Task");
    }

}

public class TimingTaskDecorator : Task
{
    Task task;
    Stopwatch stopwatch = new Stopwatch();

    public TimingTaskDecorator(Task task)
    {
        this.task = task;
    }

    public override void Run()
    {
        stopwatch.Start();
        task.Run();
        stopwatch.Stop();

        Console.WriteLine($"It took {stopwatch.Elapsed}");
    }
}

public class RetryTaskDecorator : Task
{
    Task task;
    int retries;

    public RetryTaskDecorator(Task task, int retries)
    {
        this.task = task;
        this.retries = retries;
    }

    public override void Run()
    {
        int retry = 0;
        while (retry < retries)
        {
            try
            {
                task.Run();
                break;
            }
            catch (Exception ex)
            {
                retry++;
                Console.WriteLine($"Execption caugth {ex.Message}. Retry count {retry}.");
                if (retry == retries)
                {
                    Console.WriteLine($"Max retries reached. Operation cancelled");
                }
            }
        }
    }
}

public interface ILegacyJob
{
    void ExecuteJob();
}
public class LegacyPrintJob : ILegacyJob
{
    public void ExecuteJob()
    {
        Console.WriteLine("Running legacy printing job");
    }
}

public class LegacyJobAdapater : Task
{
    private ILegacyJob job;
    public LegacyJobAdapater(ILegacyJob job)
    {
        this.job = job;
    }

    public override void Run()
    {
        job.ExecuteJob();
    }
}

public interface ICommand
{
    void Execute();
}
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

class Program
{
    static void Main()
    {
        List<string> tasks = new List<string> { "Email", "Image", "Pdf", "Image" };

        CommandQueue queue = new CommandQueue();
        IExecutionStrategy immediateStrategy = new ImmediateExecution();

        foreach (string taskType in tasks)
        {
            Task baseTask = TaskFactory.CreateTask(taskType);
            Task decoratedTask = new RetryTaskDecorator(
                         new TimingTaskDecorator(
                             new LoggingTaskDecorator(baseTask)
                         ),
                         retries: 3);
            ICommand command = new TaskCommand(decoratedTask, immediateStrategy);
            queue.Enqueue(command);
        }

        
        Task legacyTask = new LegacyJobAdapater(new LegacyPrintJob());
        Task decoratedLegacyTask = new RetryTaskDecorator(
            new TimingTaskDecorator(
                new LoggingTaskDecorator(legacyTask)
            ), retries: 3);
        ICommand legacyCommand = new TaskCommand(decoratedLegacyTask, immediateStrategy);
        queue.Enqueue(legacyCommand);
        
        queue.ExecuteAll();
    }
}