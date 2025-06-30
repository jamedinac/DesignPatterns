namespace DesignPatterns.Patterns.Observer.Configurator
{
    public class ObserverConfigurator
    {
        private ObservableTask task;

        public ObserverConfigurator(ObservableTask task)
        {
            this.task = task;
        }

        public ObserverConfigurator WithConsoleLogger()
        {
            this.task.AddObserver(new ConsoleLogger());
            return this;
        }

        public ObserverConfigurator WithMetricsLogger()
        {
            this.task.AddObserver(new MetricsLogger());
            return this;
        }

        public ObserverConfigurator WithObserver(ITaskObserver observer)
        {
            this.task.AddObserver(observer);
            return this;
        }

        public ObservableTask Configure() => this.task;
    }
}