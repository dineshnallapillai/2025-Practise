# Command Pattern

The **Command Pattern** turns a request into a standalone object that contains all the information about the request —  
so that it can be queued, logged, undone, or executed later.

## Example Analogy: Remote Control

Each button press (like `VolumeUp`, `Mute`, `PowerOn`) is a command.

The remote doesn’t know how the TV works. It just issues commands to the device.

## Intent

- **Decouple** the sender (invoker) from the receiver (executor).
- Enable **undo/redo**, **logging**, and **queueing** of operations.
- Supports **parameterizing objects with actions**.
