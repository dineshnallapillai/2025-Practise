public interface IAirTrafficControl
{
    void RegisterPlane(AirPlane plane);
    void SendInstruction(string instruction, AirPlane plane);
}

public class ControlTower : IAirTrafficControl
{
    private readonly List<AirPlane> _planes = new();

    public void RegisterPlane(AirPlane plane)
    {
        if (!_planes.Contains(plane))
        {
            _planes.Add(plane);
            plane.SetControlTower(this);
        }
    }

    public void SendInstruction(string instruction, AirPlane sender)
    {
        foreach (var plane in _planes)
        {
            if (plane != sender)
            {
                plane.ReceiveInstruction(instruction, sender);
            }
        }
    }
}

public class AirPlane
{
    public string CallSign { get; }
    private IAirTrafficControl? _controlTower;

    public AirPlane(string callSign)
    {
        CallSign = callSign;
    }

    // Called by the mediator when registering
    public void SetControlTower(IAirTrafficControl controlTower)
    {
        _controlTower = controlTower;
    }

    // Plane sends a request or message to the tower
    public void SendRequest(string instruction)
    {
        Console.WriteLine($"{CallSign} requests: {instruction}");
        _controlTower?.SendInstruction(instruction, this);
    }

    // Plane receives instruction from the tower
    public void ReceiveInstruction(string instruction, AirPlane from)
    {
        Console.WriteLine($"{CallSign} receives from {from.CallSign}: {instruction}");
    }
}



