
namespace SoftwareDesignPatterns.Creational_Pattern.FactoryMethod
{
    public class EmailNotifierFactory : NotifierFactory
    {
        public override INotifier CreateNotifier() => new EmailNotifier();
    }

}
