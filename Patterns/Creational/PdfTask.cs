namespace DesignPatterns.Patterns.Creational
{
    using DesignPatterns.Core;
    public class PdfTask : Task
    {
        public override void Run()
        {
            Console.WriteLine("This is a Pdf task running");
        }
    }
}