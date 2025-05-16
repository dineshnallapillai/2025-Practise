public class DVDPlayer
{
    public void On() => Console.WriteLine("DVD Player is ON");
    public void Play(string movie) => Console.WriteLine($"Playing '{movie}'");
}

public class SoundSystem
{
    public void On() => Console.WriteLine("Sound System is ON");
    public void SetVolume(int level) => Console.WriteLine($"Volume set to {level}");
}

public class Projector
{
    public void On() => Console.WriteLine("Projector is ON");
    public void SetInput(string source) => Console.WriteLine($"Projector input set to {source}");
}

public class Lights
{
    public void Dim(int level) => Console.WriteLine($"Lights dimmed to {level}%");
}

public class HomeTheaterFacade
{
    private readonly DVDPlayer _dvdPlayer;
    private readonly SoundSystem _soundSystem;
    private readonly Projector _projector;
    private readonly Lights _lights;

    public HomeTheaterFacade(DVDPlayer dvd, SoundSystem sound, Projector projector, Lights lights)
    {
        _dvdPlayer = dvd;
        _soundSystem = sound;
        _projector = projector;
        _lights = lights;
    }

    public void WatchMovie(string movie)
    {
        Console.WriteLine("\nGet ready to watch a movie...");
        _lights.Dim(20);
        _projector.On();
        _projector.SetInput("DVD");
        _soundSystem.On();
        _soundSystem.SetVolume(10);
        _dvdPlayer.On();
        _dvdPlayer.Play(movie);
    }
}
