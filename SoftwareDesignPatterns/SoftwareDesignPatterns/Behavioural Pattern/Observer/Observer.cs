//IObserver Interface(Trader)
public interface IStockObserver
{
    void Update(string stockSymbol, decimal price);
}

//ISubject Interface(Stock)
public interface IStock
{
    void Register(IStockObserver observer);
    void Unregister(IStockObserver observer);
    void Notify();
}

//Concrete Subject(Stock)
public class Stock : IStock
{
    private readonly List<IStockObserver> _observers = new();
    private decimal _price;

    public string Symbol { get; }

    public Stock(string symbol, decimal price)
    {
        Symbol = symbol;
        _price = price;
    }

    public void Register(IStockObserver observer) => _observers.Add(observer);

    public void Unregister(IStockObserver observer) => _observers.Remove(observer);

    public void SetPrice(decimal newPrice)
    {
        if (_price != newPrice)
        {
            Console.WriteLine($"\n[Stock] {Symbol} price changed from {_price:C} to {newPrice:C}");
            _price = newPrice;
            Notify();
        }
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(Symbol, _price);
        }
    }
}

//Concrete Observer(Trader)
public class Trader : IStockObserver
{
    private string _name { get; }

    public Trader(string name) => _name = name;

    public void Update(string stockSymbol, decimal price)
    {
        Console.WriteLine($"{_name} notified: {stockSymbol} is now {price:C}");
    }
}