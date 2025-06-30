namespace DesignPatterns.Patterns.Creational
{
    using DesignPatterns.Core;
    
    public class ImageTask : Task
    {
        public override void Run()
        {
            Console.WriteLine("This is an image task running");
        }
    }
}
