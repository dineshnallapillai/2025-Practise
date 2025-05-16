using SoftwareDesignPatterns.Creational_Pattern.Builder;

public interface ICarPrototype
{
    Car Clone();
}

public class FeaturePackage
{
    public bool HeatedSeats { get; set; }
    public bool GPS { get; set; }

    public FeaturePackage DeepCopy()
    {
        return new FeaturePackage
        {
            HeatedSeats = this.HeatedSeats,
            GPS = this.GPS
        };
    }
}
public class Car : ICarPrototype
{
    public string Model { get; set; }
    public string Engine { get; set; }
    public string Transmission { get; set; }
    public bool HasSunroof { get; set; }

    public FeaturePackage Features { get; set; } // <-- Reference type
    public void ShowDetails()
    {
        Console.WriteLine($"🚗 {Model} | Engine: {Engine}, Transmission: {Transmission}, Sunroof: {(HasSunroof ? "Yes" : "No")}");
    }

    public Car Clone()
    {
        // Deep copy
        return new Car
        {
            Model = this.Model,
            Engine = this.Engine,
            Transmission = this.Transmission,
            HasSunroof = this.HasSunroof,
            Features = this.Features?.DeepCopy() // Deep copy reference property
        };
    }
}