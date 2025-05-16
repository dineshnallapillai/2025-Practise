//1.Abstract Product Interfaces
public interface IButton
{
    void Render();
}
public interface ITextbox
{
    void Render();
}

// 2.Concrete Products

// Windows Controls
public class WindowsButton : IButton
{
    public void Render() => Console.WriteLine("Rendering Windows Button");
}

public class WindowsTextbox : ITextbox
{
    public void Render() => Console.WriteLine("Rendering Windows Textbox");
}

// Mac Controls
public class MacButton : IButton
{
    public void Render() => Console.WriteLine("Rendering Mac Button");
}

public class MacTextbox : ITextbox
{
    public void Render() => Console.WriteLine("Rendering Mac Textbox");
}

// 3.Abstract Factory

public interface IUIFactory
{
    IButton CreateButton();
    ITextbox CreateTextbox();
}

// 4.Concrete Factories
public class WindowsUIFactory : IUIFactory
{
    public IButton CreateButton() => new WindowsButton();
    public ITextbox CreateTextbox() => new WindowsTextbox();
}

public class MacUIFactory : IUIFactory
{
    public IButton CreateButton() => new MacButton();
    public ITextbox CreateTextbox() => new MacTextbox();
}

// 5.Client Code

public class Application
{
    private readonly IButton _button;
    private readonly ITextbox _textbox;

    public Application(IUIFactory factory)
    {
        _button = factory.CreateButton();
        _textbox = factory.CreateTextbox();
    }

    public void RenderUI()
    {
        _button.Render();
        _textbox.Render();
    }
}

// Use Windows UI
var windowsApp = new Application(new WindowsUIFactory());
windowsApp.RenderUI();

// Use Mac UI
var macApp = new Application(new MacUIFactory());
macApp.RenderUI();