namespace DesignPatterns.Patterns.Creational
{
    using DesignPatterns.Core;
    
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
}