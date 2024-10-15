using System;

namespace Library.Contracts;

public interface OrderCancellationRequested
{
    Guid OrderId { get; }
    public DateTime Timestamp { get; }
}