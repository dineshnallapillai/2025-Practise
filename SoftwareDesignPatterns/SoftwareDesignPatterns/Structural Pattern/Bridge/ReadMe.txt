The Bridge pattern is used to decouple an abstraction from its implementation, so the two can evolve independently.


You're building chargers for mobile devices. The charging logic is the same, but the plug type differs between countries:


Charger is the abstraction.

IPlug is the implementation interface.

USPlug and EuropeanPlug are concrete implementors.

Both evolve independently — you can add new plug types (e.g., UK, India) without touching Charger.

