
namespace SoftwareDesignPatterns.Creational_Pattern.Singleton
{
    public sealed class Logger
    {
        private static readonly Lazy<Logger> _instance = new(()=>new Logger());

        private Logger() { }

        public static Logger Instance => _instance.Value;

        public void LogMessage(string message)
        {
            Console.WriteLine($"[LOG]: {message}");

        }
    }
}
