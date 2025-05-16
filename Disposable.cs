using System;
using System.IO;
using System.Runtime.InteropServices;

public class MyResourceHandler : IDisposable
{
    private bool _disposed = false;

    // Managed resource
    private readonly FileStream _fileStream;

    // Simulated unmanaged resource
    private IntPtr _unmanagedMemory;

    public MyResourceHandler(string filePath)
    {
        _fileStream = new FileStream(filePath, FileMode.OpenOrCreate);

        // Allocate 100 bytes of unmanaged memory
        _unmanagedMemory = Marshal.AllocHGlobal(100);
    }

    // Public implementation of Dispose pattern callable by consumers.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            // Dispose managed resources
            _fileStream?.Dispose();
        }

        // Free unmanaged resources
        if (_unmanagedMemory != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(_unmanagedMemory);
            _unmanagedMemory = IntPtr.Zero;
        }

        _disposed = true;
    }

    // Finalizer (only used if Dispose was not called)
    ~MyResourceHandler()
    {
        Dispose(false);
    }
}
