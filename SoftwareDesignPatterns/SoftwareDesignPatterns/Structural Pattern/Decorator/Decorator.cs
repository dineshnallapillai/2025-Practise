using System.Reflection.Metadata.Ecma335;

public interface ICoffee
{ 
    string GetDescription();
    double GetCost();
}

public class BasicCoffee : ICoffee
{
    public string GetDescription() => "Plain Coffee";
    public double GetCost() => 1.0;

}

public abstract class CoffeeDecorator : ICoffee
{
    protected readonly ICoffee _coffee;

    protected CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    public virtual string GetDescription() => _coffee.GetDescription();
    public virtual double GetCost() => _coffee.GetCost();
}


public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee) : base(coffee) { }

    public override string GetDescription() => _coffee.GetDescription() + "Milk Added";

    public override double GetCost() => _coffee.GetCost() + 0.5;
}

public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee) : base(coffee) { }

    public override string GetDescription() => _coffee.GetDescription() + "Sugar Added";
    public override double GetCost() => _coffee.GetCost() + 0.3;
}