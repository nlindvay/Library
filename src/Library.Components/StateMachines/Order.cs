using System;
using System.Collections.Generic;
using MassTransit;

namespace Library.Components.StateMachines;

public class Order : SagaStateMachineInstance
{
    public int CurrentState { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public string PrimaryReference { get; set; }
    public string SecondaryReference { get; set; }
    public DateTimeOffset RequiredBy { get; set; }
    public List<EventLog> Events { get; set; } = new();
    public Guid CorrelationId { get; set; }
}

public class EventLog
{
    public Guid? EventId { get; set; }
    public string Message { get; set; }
}