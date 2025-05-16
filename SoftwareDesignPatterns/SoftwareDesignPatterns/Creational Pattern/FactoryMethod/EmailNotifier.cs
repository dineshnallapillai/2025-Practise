
namespace SoftwareDesignPatterns.Creational_Pattern.FactoryMethod
{
    public class EmailNotifier : INotifier
    {
        public void Send(string message)
        {
            Console.WriteLine($"📧 Email: {message}");
        }
    }
}
