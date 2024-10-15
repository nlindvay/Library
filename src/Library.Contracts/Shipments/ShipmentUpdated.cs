using System;

namespace Library.Contracts;

public interface ShipmentUpdated
{
    Guid ShipmentId { get; }
    public DateTime Timestamp { get; }
    public string PrimaryReference { get; }
    public string SecondaryReference { get; }
    public DateTimeOffset RequiredBy { get; }
}