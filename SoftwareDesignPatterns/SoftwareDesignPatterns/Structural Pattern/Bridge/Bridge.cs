
public interface IPlug
{ 
    void ConnectToPower();
}

public class USPlug : IPlug
{
    public void ConnectToPower() => Console.WriteLine("🔌 Connected to US Power Supply (110V)");
}

public class EuropeanPlug : IPlug
{
    public void ConnectToPower() => Console.WriteLine("🔌 Connected to European Power Supply (220V)");
}

//Abstraction(Charger)
public class Charger
{
    private readonly IPlug _plug;

    public Charger(IPlug plug)
    {
        _plug = plug;
    }

    public void ChargePhone()
    {
        _plug.ConnectToPower();
        Console.WriteLine("⚡ Charging phone...");
    }
}

//Client code
var usCharger = new Charger(new USPlug());
usCharger.ChargePhone();

Console.WriteLine();

var europeanCharger = new Charger(new EuropeanPlug());
europeanCharger.ChargePhone();
