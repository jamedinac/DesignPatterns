namespace DesignPatterns.Core
{
    public class EmailTask : Task
    {
        public override void Run()
        {
            Console.WriteLine("This is an Email task running");
        }
    }
}