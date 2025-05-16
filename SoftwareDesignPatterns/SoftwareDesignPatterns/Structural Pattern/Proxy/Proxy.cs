public interface IImage
{
    void Display();
}

public class RealImage : IImage
{
    private readonly string _filename;

    public RealImage(string filename)
    {
        _filename = filename;
        LoadImageFromDisk(); // Heavy operation
    }

    private void LoadImageFromDisk()
    {
        Console.WriteLine($"Loading image from disk: {_filename}");
    }

    public void Display()
    {
        Console.WriteLine($"Displaying image: {_filename}");
    }
}

public class ProxyImage : IImage
{
    private readonly string _filename;
    private RealImage? _realImage;

    public ProxyImage(string filename)
    {
        _filename = filename;
    }

    public void Display()
    {
        if (_realImage == null)
        {
            _realImage = new RealImage(_filename); // Lazy initialization
        }
        _realImage.Display();
    }
}
