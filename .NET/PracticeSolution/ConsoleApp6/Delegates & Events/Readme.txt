What is a Delegate?
A delegate is a type-safe function pointer — it can point to any method with a matching signature

public delegate void MyDelegate(string message);
void Print(string msg) => Console.WriteLine(msg);
MyDelegate del = Print;
del("Hello from delegate!");

Built-in Delegate Types
Instead of creating custom delegate types, you can use these:

Delegate Type	       Signature	Description
Action	               void()	     No return value
Action<T>	           void(T)	     Takes one input
Func<T>	               T()	         Returns a value
Func<T, TResult>	   T → TResult	 Takes input, returns output
Predicate<T>	       T → bool	     Returns a boolean

Multicast Delegates
A multicast delegate can hold references to multiple methods.

All methods are invoked in order, but only the result of the last method is returned (if it’s not void).

Events & EventHandler Pattern in C#
In C#, events allow a class (the publisher) to notify other classes (subscribers) when something significant happens. 
They are built on top of delegates, which act as function pointers. Events provide a mechanism for loosely coupled 
communication between objects, enabling the publisher to raise an event without knowing who, if anyone, is listening. 

