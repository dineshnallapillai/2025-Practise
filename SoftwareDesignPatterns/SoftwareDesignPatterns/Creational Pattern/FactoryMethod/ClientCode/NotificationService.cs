
namespace SoftwareDesignPatterns.Creational_Pattern.FactoryMethod.ClientCode
{
    public class NotificationService
    {
        private NotifierFactory _notifierFactory;

        public NotificationService(NotifierFactory factory)
        {
            _notifierFactory = factory;
        }

        public void Alert(string message)
        {
            var notifier = _notifierFactory.CreateNotifier();
            notifier.Send(message);
        }
    }
}
