public class TempChangedEventArgs : EventArgs
{
    public double Temperature { get; }
    public TempChangedEventArgs(double temp) => Temperature = temp;
}

public class TemperatureSensor
{
    public event EventHandler<TempChangedEventArgs> TemperatureChanged;

    public void UpdateTemperature(double newTemp)
    {
        TemperatureChanged?.Invoke(this, new TempChangedEventArgs(newTemp));
    }
}