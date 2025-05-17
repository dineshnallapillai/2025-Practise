# Creational Patterns

## Singleton

**Purpose**: Ensures a class has only one instance and provides a global point of access to it.

```csharp
public class Singleton
{
    private static Singleton _instance;
    private Singleton() { }
    
    public static Singleton Instance => _instance ??= new Singleton();
}
```

## Factory Method

**Purpose**: Defines an interface for creating an object, but allows subclasses to alter the type of objects that will be created.

```csharp
public interface IProduct { }
public class ConcreteProductA : IProduct { }
public class ConcreteProductB : IProduct { }

public abstract class Creator
{
    public abstract IProduct FactoryMethod();
}

public class ConcreteCreatorA : Creator
{
    public override IProduct FactoryMethod() => new ConcreteProductA();
}
```

## Abstract Factory

**Purpose**: Provides an interface for creating families of related or dependent objects without specifying their concrete classes.

```csharp
public interface IButton { }
public class WindowsButton : IButton { }
public class MacButton : IButton { }

public interface IGUIFactory
{
    IButton CreateButton();
}

public class WindowsFactory : IGUIFactory
{
    public IButton CreateButton() => new WindowsButton();
}
```

## Prototype

**Purpose**: Specifies the kind of objects to create using a prototypical instance, and create new objects by copying this prototype.

```csharp
public class Prototype : ICloneable
{
    public string Name { get; set; }
    public object Clone() => MemberwiseClone();
}
```

## Builder

**Purpose**: Separates the construction of a complex object from its representation so that the same construction process can create different representations.

```csharp
public class Car
{
    public string Model { get; set; }
    public string Engine { get; set; }
}

public class CarBuilder
{
    private readonly Car _car = new Car();
    public CarBuilder SetModel(string model) { _car.Model = model; return this; }
    public CarBuilder SetEngine(string engine) { _car.Engine = engine; return this; }
    public Car Build() => _car;
}
```

# Structural Patterns

## Adapter

**Purpose**: Converts the interface of a class into another interface clients expect.

```csharp
public interface ITarget { void Request(); }
public class Adaptee { public void SpecificRequest() { Console.WriteLine("Specific Request"); } }

public class Adapter : ITarget
{
    private readonly Adaptee _adaptee;
    public Adapter(Adaptee adaptee) => _adaptee = adaptee;
    public void Request() => _adaptee.SpecificRequest();
}
```

## Bridge

**Purpose**: Decouples an abstraction from its implementation so that the two can vary independently.

```csharp
public interface IEngine { void Start(); }
public class PetrolEngine : IEngine { public void Start() { Console.WriteLine("Starting petrol engine..."); } }
public class ElectricEngine : IEngine { public void Start() { Console.WriteLine("Starting electric engine..."); } }

public abstract class Vehicle
{
    protected IEngine _engine;
    public Vehicle(IEngine engine) => _engine = engine;
    public abstract void Drive();
}

public class Car : Vehicle
{
    public Car(IEngine engine) : base(engine) { }
    public override void Drive() => _engine.Start();
}
```

## Composite

**Purpose**: Composes objects into tree-like structures to represent part-whole hierarchies.

```csharp
public interface IComponent { void Operation(); }
public class Leaf : IComponent { public void Operation() { Console.WriteLine("Leaf Operation"); } }
public class Composite : IComponent
{
    private readonly List<IComponent> _children = new List<IComponent>();
    public void Add(IComponent component) => _children.Add(component);
    public void Operation() => _children.ForEach(child => child.Operation());
}
```

## Decorator

**Purpose**: Attach additional responsibilities to an object dynamically.

```csharp
public interface ICoffee { string GetDescription(); double GetCost(); }
public class SimpleCoffee : ICoffee { public string GetDescription() => "Simple Coffee"; public double GetCost() => 5; }
public class MilkDecorator : ICoffee
{
    private readonly ICoffee _coffee;
    public MilkDecorator(ICoffee coffee) => _coffee = coffee;
    public string GetDescription() => _coffee.GetDescription() + ", Milk";
    public double GetCost() => _coffee.GetCost() + 2;
}
```

## Facade

**Purpose**: Provides a simplified interface to a complex subsystem.

```csharp
public class SubSystemA { public void MethodA() { Console.WriteLine("Method A"); } }
public class SubSystemB { public void MethodB() { Console.WriteLine("Method B"); } }
public class Facade
{
    private readonly SubSystemA _subSystemA = new SubSystemA();
    private readonly SubSystemB _subSystemB = new SubSystemB();
    public void Operation() { _subSystemA.MethodA(); _subSystemB.MethodB(); }
}
```

## Flyweight

**Purpose**: Reduces memory usage by sharing as much data as possible with similar objects.

```csharp
public interface IChessPiece { void Display(Position position); }
public class ChessPiece : IChessPiece
{
    private readonly string _type;
    private readonly string _color;
    public ChessPiece(string type, string color) { _type = type; _color = color; }
    public void Display(Position position) => Console.WriteLine($"{_color} {_type} at ({position.X}, {position.Y})");
}

public class Position { public int X { get; set; } public int Y { get; set; } }
```

# Behavioral Patterns

## Chain of Responsibility

**Purpose**: Allows multiple objects to handle a request, without the sender needing to know which object will handle it.

```csharp
public abstract class Handler
{
    protected Handler _next;
    public void SetNext(Handler next) => _next = next;
    public abstract void HandleRequest(int amount);
}

public class ConcreteHandlerA : Handler
{
    public override void HandleRequest(int amount)
    {
        if (amount < 100) Console.WriteLine("Handled by A");
        else _next?.HandleRequest(amount);
    }
}
```

## Command

**Purpose**: Encapsulates a request as an object, allowing for parameterization of clients with different requests.

```csharp
public interface ICommand { void Execute(); }
public class LightOnCommand : ICommand
{
    private readonly Light _light;
    public LightOnCommand(Light light) => _light = light;
    public void Execute() => _light.TurnOn();
}
```

## Iterator

**Purpose**: Provides a way to access elements of a collection without exposing the underlying structure.

```csharp
public class EmployeeCollection : IEnumerable<Employee>
{
    private readonly List<Employee> _employees = new List<Employee>();
    public void Add(Employee employee) => _employees.Add(employee);
    public IEnumerator<Employee> GetEnumerator() => _employees.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
```

## Observer

**Purpose**: Defines a one-to-many dependency between objects, so when one object changes state, all its dependents are notified.

```csharp
public class Stock
{
    private readonly List<IObserver> _observers = new List<IObserver>();
    public void AddObserver(IObserver observer) => _observers.Add(observer);
    public void NotifyObservers() => _observers.ForEach(o => o.Update());
}
```

## State

**Purpose**: Allows an object to change its behavior when its internal state changes.

```csharp
public class OrderContext
{
    private IOrderState _state;
    public void SetState(IOrderState state) => _state = state;
    public void ProcessOrder() => _state.ProcessOrder(this);
}
```

## Strategy

**Purpose**: Defines a family of algorithms, encapsulates each one, and makes them interchangeable.

```csharp
public interface IStrategy { void Execute(); }
public class ConcreteStrategyA : IStrategy { public void Execute() => Console.WriteLine("Strategy A"); }
public class Context { private IStrategy _strategy; public void SetStrategy(IStrategy strategy) => _strategy = strategy; }
```

## Template Method

**Purpose**: Defines the skeleton of an algorithm in the method, deferring some steps to subclasses.

```csharp
public abstract class AbstractClass
{
    public void TemplateMethod()
    {
        Step1();
        Step2();
    }
    protected abstract void Step1();
    protected abstract void Step2();
}
```
