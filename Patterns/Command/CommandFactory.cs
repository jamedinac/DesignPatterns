using DesignPatterns.Core;

namespace DesignPatterns.Patterns.Command
{
    using DesignPatterns.Patterns.Creational;
    using Task = DesignPatterns.Core.Task;

    public static class CommandFactory
    {
        public static ICommand Create(string taskType, IExecutionStrategy strategy)
            => Create(TaskFactory.CreateTask(taskType), strategy);

        public static ICommand Create(Task baseTask, IExecutionStrategy strategy)
            => new TaskCommand(PipelineFactory.BuildPipeline(baseTask), strategy);
    }
}