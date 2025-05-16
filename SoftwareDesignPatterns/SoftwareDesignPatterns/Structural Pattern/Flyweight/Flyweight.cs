//Flyweight Interface
public interface IChessPiece
{
    void Display(Position position);
}

//Concrete Flyweight
public class ChessPiece : IChessPiece
{
    private readonly string _type;
    private readonly string _color;

    public ChessPiece(string type, string color)
    {
        _type = type;
        _color = color;
    }

    public void Display(Position position)
    {
        Console.WriteLine($"{_color} {_type} placed at ({position.X}, {position.Y})");
    }
}

//Flyweight Factory
public class ChessPieceFactory
{
    private readonly Dictionary<string, IChessPiece> _pieces = new();

    public IChessPiece GetChessPiece(string type, string color)
    {
        string key = $"{color}_{type}";
        if (!_pieces.ContainsKey(key))
        {
            _pieces[key] = new ChessPiece(type, color);
            Console.WriteLine($"Creating new ChessPiece: {color} {type}");
        }
        return _pieces[key];
    }
}

//Position Class(Extrinsic State)
public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}
