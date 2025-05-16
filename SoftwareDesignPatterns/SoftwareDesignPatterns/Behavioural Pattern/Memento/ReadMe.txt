The Memento Pattern is used to capture and store the internal state of an object so that it can be restored later, 
without violating encapsulation.

🕹️ Think of it like Undo/Redo functionality in editors. You save the current state (a memento) and can restore it later.

When to Use

You need to implement undo/redo functionality.

You want to snapshot the internal state of an object and restore it later.

The object’s state should be preserved without exposing its internal structure.