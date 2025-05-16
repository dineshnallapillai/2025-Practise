//State Interface
public interface IOrderState
{
    void ProcessOrder(OrderContext context);
}

public class NewState : IOrderState
{
    public void ProcessOrder(OrderContext context)
    {
        Console.WriteLine("Order is now being processed.");
        context.SetState(new ProcessingState());
    }
}

//Concrete States
public class ProcessingState : IOrderState
{
    public void ProcessOrder(OrderContext context)
    {
        Console.WriteLine("Order has been shipped.");
        context.SetState(new ShippedState());
    }
}

public class ShippedState : IOrderState
{
    public void ProcessOrder(OrderContext context)
    {
        Console.WriteLine("Order has been delivered.");
        context.SetState(new DeliveredState());
    }
}

public class DeliveredState : IOrderState
{
    public void ProcessOrder(OrderContext context)
    {
        Console.WriteLine("Order is already delivered. No further action.");
    }
}

//Context Class

public class OrderContext
{
    private IOrderState _state;

    public OrderContext()
    {
        _state = new NewState(); // Default state
    }

    public void SetState(IOrderState state)
    {
        _state = state;
    }

    public void ProcessOrder()
    {
        _state.ProcessOrder(this);
    }
}

