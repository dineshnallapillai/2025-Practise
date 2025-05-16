public abstract class LeaveHandler
{
    protected LeaveHandler? _nextHandler;

    protected void SetNext(LeaveHandler handler) => _nextHandler = handler;
    public abstract void HandleRequest(int days);

}

class TeamLead : LeaveHandler
{
    public override void HandleRequest(int days)
    {
        if (days < 3)
        {
            Console.WriteLine("Team Lead approved the leave request.");
        }
        else
        {
            _nextHandler?.HandleRequest(days);
        }

    }
}

public class Manager : LeaveHandler
{
    public override void HandleRequest(int days)
    {
        if (days <= 5)
        {
            Console.WriteLine("Manager approved the leave request.");
        }
        else
        {
            _nextHandler?.HandleRequest(days);
        }
    }
}

public class Director : LeaveHandler
{
    public override void HandleRequest(int days)
    {
        if (days <= 10)
        {
            Console.WriteLine("Director approved the leave request.");
        }
        else
        {
            Console.WriteLine("Leave request denied. Too many days.");
        }
    }
}

// Create handlers
var teamLead = new TeamLead();
var manager = new Manager();
var director = new Director();

// Chain them
teamLead.SetNext(manager);
manager.SetNext(director);

// Simulate requests
teamLead.HandleRequest(1);   // Handled by Team Lead
teamLead.HandleRequest(4);   // Handled by Manager
teamLead.HandleRequest(8);   // Handled by Director
teamLead.HandleRequest(15);  // Denied