# Factory Method Pattern

The Factory Method Pattern is a Creational Design Pattern used to create objects without exposing the instantiation logic to the client. Instead, the object creation logic is abstracted in a factory method.


## Problem It Solves

You have a base class (or interface) and want to allow subclasses to decide which specific object gets created without  
modifying client code.

```csharp

public static class ShapeFactory
{
    public static IShape GetShape(string shapeType)
    {
        switch (shapeType.ToLower())
        {
            case "circle": return new Circle();
            case "square": return new Square();
            default: throw new ArgumentException("Invalid shape type");
        }
    }
}

```
