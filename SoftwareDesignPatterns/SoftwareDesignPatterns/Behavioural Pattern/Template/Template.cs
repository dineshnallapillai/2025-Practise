//Abstract Class
using System;

namespace TemplatePatternPayments
{
    // 🧱 Abstract class with template method
    public abstract class PaymentProcessor
    {
        public void ProcessPayment()
        {
            Authenticate();
            ExecutePayment();
            SendConfirmation();
        }

        protected abstract void Authenticate();
        protected abstract void ExecutePayment();

        protected virtual void SendConfirmation()
        {
            Console.WriteLine("📧 Payment confirmation sent to the user.");
        }
    }

    // 🟦 Concrete class: PayPal
    public class PayPalProcessor : PaymentProcessor
    {
        protected override void Authenticate()
        {
            Console.WriteLine("🔐 Authenticating with PayPal credentials...");
        }

        protected override void ExecutePayment()
        {
            Console.WriteLine("💸 Processing payment via PayPal...");
        }
    }

    // 🟩 Concrete class: Stripe
    public class StripeProcessor : PaymentProcessor
    {
        protected override void Authenticate()
        {
            Console.WriteLine("🔐 Authenticating with Stripe token...");
        }

        protected override void ExecutePayment()
        {
            Console.WriteLine("💳 Charging card via Stripe...");
        }
    }

    // 🧪 Client
    class Program
    {
        static void Main()
        {
            PaymentProcessor paypal = new PayPalProcessor();
            paypal.ProcessPayment();

            Console.WriteLine();

            PaymentProcessor stripe = new StripeProcessor();
            stripe.ProcessPayment();
        }
    }
}
