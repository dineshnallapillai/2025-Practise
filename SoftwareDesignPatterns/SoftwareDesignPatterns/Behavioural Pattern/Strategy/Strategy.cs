//Strategy Interface

public interface IPaymentStrategy
{
    void Pay(decimal amount);
}

//Concrete Strategies
public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount:C} using Credit Card.");
    }
}

public class PayPalPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount:C} using PayPal.");
    }
}

public class CryptoPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paid {amount:C} using Cryptocurrency.");
    }
}


//Context Class

public class PaymentProcessor
{
    private IPaymentStrategy _paymentStrategy;

    public PaymentProcessor(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void ProcessPayment(decimal amount)
    {
        _paymentStrategy.Pay(amount);
    }
}
class Program
{
    static void Main()
    {
        var processor = new PaymentProcessor(new CreditCardPayment());
        processor.ProcessPayment(100.00m);

        processor.SetPaymentStrategy(new PayPalPayment());
        processor.ProcessPayment(200.00m);

        processor.SetPaymentStrategy(new CryptoPayment());
        processor.ProcessPayment(300.00m);
    }
}