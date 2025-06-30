namespace DesignPatterns.Patterns.Creational.Builder
{
    using DesignPatterns.Core;
    using DesignPatterns.Decorators;

    public class TaskBuilder
    {
        private Task task;

        public TaskBuilder(Task task)
        {
            this.task = task;
        }

        public TaskBuilder WithLogging()
        {
            this.task = new LoggingTaskDecorator(this.task);
            return this;
        }

        public TaskBuilder WithRetry(int retries)
        {
            this.task = new RetryTaskDecorator(this.task, retries);
            return this;
        }

        public TaskBuilder WithTiming()
        {
            this.task = new TimingTaskDecorator(this.task);
            return this;
        }

        public Task Build() => this.task;
    }
}