using System;

namespace Library.Contracts;

public interface OrderUpdated
{
    Guid OrderId { get; }
    public DateTime Timestamp { get; }
    public string PrimaryReference { get; }
    public string SecondaryReference { get; }
    public DateTimeOffset RequiredBy { get; }
}
