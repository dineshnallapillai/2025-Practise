namespace SoftwareDesignPatterns.Creational_Pattern.FactoryMethod
{
    public class SmsNotifierFactory : NotifierFactory
    {
        public override INotifier CreateNotifier() => new SmsNotifier();
    }
}
