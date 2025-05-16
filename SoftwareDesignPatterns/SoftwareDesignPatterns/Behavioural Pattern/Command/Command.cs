//Command Interface
public interface ICommand
{
    void Execute();
    void Undo();
}

//Receivers(TV & Light)
public class Light
{
    public void TurnOn() => Console.WriteLine("Light is ON");
    public void TurnOff() => Console.WriteLine("Light is OFF");
}

public class TV
{
    public void TurnOn() => Console.WriteLine("TV is ON");
    public void TurnOff() => Console.WriteLine("TV is OFF");
}

//Concrete Commands

public class LightOnCommand : ICommand
{
    private readonly Light _light;
    public LightOnCommand(Light light) => _light = light;

    public void Execute() => _light.TurnOn();
    public void Undo() => _light.TurnOff();
}

public class TVOnCommand : ICommand
{
    private readonly TV _tv;
    public TVOnCommand(TV tv) => _tv = tv;

    public void Execute() => _tv.TurnOn();
    public void Undo() => _tv.TurnOff();
}

//Invoker(Remote Control)

public class RemoteControl
{
    private readonly Stack<ICommand> _history = new();

    public void PressButton(ICommand command)
    {
        command.Execute();
        _history.Push(command);
    }

    public void PressUndo()
    {
        if (_history.TryPop(out var lastCommand))
        {
            lastCommand.Undo();
        }
        else
        {
            Console.WriteLine("Nothing to undo.");
        }
    }
}
