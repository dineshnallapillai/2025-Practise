namespace SoftwareDesignPatterns.Creational_Pattern.Builder
{   
    //Complex Object
    public class Car
    {
        public string Engine { get; set; } 
        public string Transmission { get; set; }
        public int Wheels { get; set; }
        public bool HasSunroof { get; set; }
        public bool HasGPS { get; set; }

        public void ShowSpecifications()
        {
            Console.WriteLine("🚘 Car Specifications:");
            Console.WriteLine($"- Engine: {Engine}");
            Console.WriteLine($"- Wheels: {Wheels}");
            Console.WriteLine($"- Sunroof: {(HasSunroof ? "Yes" : "No")}");
            Console.WriteLine($"- GPS: {(HasGPS ? "Yes" : "No")}");
            Console.WriteLine($"- Transmission: {Transmission}");
        }
    }

    // Builder Interface
    public interface ICarBuilder
    {
        void SetEngine(string engine);
        void SetWheels(int count);
        void AddSunroof();
        void AddGPS();
        void SetTransmission(string transmission);
        Car Build();
    }

    //Concrete Builder 
    public class CarBuilder : ICarBuilder
    {
        private readonly Car _car = new();

        public void SetEngine(string engine) => _car.Engine = engine;
        public void SetWheels(int count) => _car.Wheels = count;
        public void AddSunroof() => _car.HasSunroof = true;
        public void AddGPS() => _car.HasGPS = true;
        public void SetTransmission(string transmission) => _car.Transmission = transmission;

        public Car Build() => _car;
    }


    //Director (Optional) 
    public class CarDirector
    {
        private readonly ICarBuilder _builder;

        public CarDirector(ICarBuilder builder)
        {
            _builder = builder;
        }

        public void BuildSportsCar()
        {
            _builder.SetEngine("V8");
            _builder.SetWheels(4);
            _builder.AddSunroof();
            _builder.AddGPS();
            _builder.SetTransmission("Automatic");
        }

        public void BuildSUV()
        {
            _builder.SetEngine("V6");
            _builder.SetWheels(4);
            _builder.AddGPS();
            _builder.SetTransmission("Manual");
        }
    }


}
