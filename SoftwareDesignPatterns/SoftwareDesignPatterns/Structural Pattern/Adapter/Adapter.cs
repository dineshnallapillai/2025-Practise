public interface IEuropeanSocket
{
    void ProvideElectricity(); // Round pin
}

//Incompatible Device(US plug)

public class USPlugDevice
{
    public void PlugInFlatPin()
    {
        Console.WriteLine("🔌 US plug device is now powered using flat-pin electricity.");
    }
}

//Adapter(EU to US)
public class USToEuropeanAdapter : IEuropeanSocket
{
    private readonly USPlugDevice _usDevice;

    public USToEuropeanAdapter(USPlugDevice usDevice)
    {
        _usDevice = usDevice;
    }

    public void ProvideElectricity()
    {
        Console.WriteLine("🛠 Adapting round pin to flat pin...");
        _usDevice.PlugInFlatPin();
    }
}


//Client Code
public class EuropeanHotel
{
    private readonly IEuropeanSocket _socket;

    public EuropeanHotel(IEuropeanSocket socket)
    {
        _socket = socket;
    }

    public void PowerDevice()
    {
        Console.WriteLine("🏨 Hotel socket supplying power:");
        _socket.ProvideElectricity();
    }
}