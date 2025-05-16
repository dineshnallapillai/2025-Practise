namespace SoftwareDesignPatterns.Creational_Pattern.FactoryMethod
{
    public class SmsNotifier : INotifier
    {
        public void Send(string message)
        {
            Console.WriteLine($"📱 SMS: {message}");
        }
    }
}
