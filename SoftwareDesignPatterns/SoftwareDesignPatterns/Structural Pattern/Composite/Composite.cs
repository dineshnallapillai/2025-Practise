// 🧱 Component
public abstract class Employee
{
    public string Name { get; }
    public string Position { get; }

    protected Employee(string name, string position)
    {
        Name = name;
        Position = position;
    }

    public abstract void Display(int indent = 0);
}

// 📄 Leaf
public class Developer : Employee
{
    public Developer(string name) : base(name, "Developer") { }

    public override void Display(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + $"- 👨‍💻 {Position}: {Name}");
    }
}

// 📄 Leaf
public class Designer : Employee
{
    public Designer(string name) : base(name, "Designer") { }

    public override void Display(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + $"- 🎨 {Position}: {Name}");
    }
}

// 📁 Composite
public class Manager : Employee
{
    private readonly List<Employee> _team = [];

    public Manager(string name) : base(name, "Manager") { }

    public void AddTeamMember(Employee employee)
    {
        _team.Add(employee);
    }

    public void RemoveTeamMember(Employee employee)
    {
        _team.Remove(employee);
    }

    public override void Display(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + $"+ 🧑‍💼 {Position}: {Name}");
        foreach (var e in _team)
        {
            e.Display(indent + 4);
        }
    }
}