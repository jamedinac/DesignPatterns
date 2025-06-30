namespace DesignPatterns.Patterns.Creational
{
    using DesignPatterns.Patterns.Observer.Configurator;
    using DesignPatterns.Patterns.Observer;

    using Task = DesignPatterns.Core.Task;
    using DesignPatterns.Patterns.Creational.Builder;

    public static class PipelineFactory
    {
        public static Task BuildPipeline(Task baseTask, int retries = 3)
            => new ObserverConfigurator(
                        new ObservableTask(
                            new TaskBuilder(baseTask)
                            .WithLogging()
                            .WithTiming()
                            .WithRetry(retries)
                            .Build()
                        ))
                        .WithConsoleLogger()
                        .WithMetricsLogger()
                        .Configure();
    }
}