using System.Text.Json;
using System.Text.Json.Serialization;
using Library.Contracts;
using MassTransit;


namespace Library.Components.StateMachines;

public sealed class OrderStateMachine :
    MassTransitStateMachine<Order>
{
    static OrderStateMachine()
    {
        MessageContracts.Initialize();
    }

    public OrderStateMachine()
    {
        InstanceState(x => x.CurrentState, Entered, Processing, Cancelled, Finalized);

        Event(() => Updated, x => x.CorrelateById(m => m.Message.OrderId));
        Event(() => CancellationRequested, x => x.CorrelateById(m => m.Message.OrderId));

        Initially(
            When(Added)
                .MapDataToInstance()
                .TransitionTo(Entered)
                .PublishOrderToWMS());


        During(Entered,
            When(CancellationRequested)
                .PublishCancellationRequest());

        During(Entered,
            When(Updated)
                .MapDataToInstance()
                .PublishOrderToWMS());

        During(Processing,
            When(CancellationRequested)
                .PublishCancellationRequest());

        During(Processing,
            When(Updated)
                .MapDataToInstance()
                .PublishOrderToWMS());
    }

    public Event<OrderAdded> Added { get; }
    public Event<OrderCancellationRequested> CancellationRequested { get; }
    public Event<OrderUpdated> Updated { get; }

    public State Entered { get; }
    public State Processing { get; }
    public State Cancelled { get; }
    public State Finalized { get; }
}


public static class OrderStateMachineExtensions
{
    public static EventActivityBinder<Order, OrderAdded> MapDataToInstance(this EventActivityBinder<Order, OrderAdded> binder)
    {
        return binder.Then(x =>
        {
            x.Saga.CreatedOn = x.Message.Timestamp;
            x.Saga.PrimaryReference = x.Message.PrimaryReference;
            x.Saga.SecondaryReference = x.Message.SecondaryReference;
            x.Saga.RequiredBy = x.Message.RequiredBy;
            x.Saga.Events.Add(new EventLog { EventId = x.MessageId, Message = JsonSerializer.Serialize(x.Message) });
        });
    }

    public static EventActivityBinder<Order, OrderUpdated> MapDataToInstance(this EventActivityBinder<Order, OrderUpdated> binder)
    {
        return binder.Then(x =>
        {
            x.Saga.Events.Add(new EventLog { EventId = x.MessageId, Message = JsonSerializer.Serialize(x.Message) });
        });
    }

    public static EventActivityBinder<Order, OrderCancellationRequested> PublishCancellationRequest(this EventActivityBinder<Order, OrderCancellationRequested> binder)
    {
        return binder.Then(x =>
        {
            x.Saga.Events.Add(new EventLog { EventId = x.MessageId, Message = JsonSerializer.Serialize(x.Message) });
        });
    }

    public static EventActivityBinder<Order, OrderAdded> PublishOrderToWMS(this EventActivityBinder<Order, OrderAdded> binder)
    {
        return binder.Then(x =>
        {
        });
    }

    public static EventActivityBinder<Order, OrderUpdated> PublishOrderToWMS(this EventActivityBinder<Order, OrderUpdated> binder)
    {
        return binder.Then(x =>
        {
        });
    }

    public static EventActivityBinder<Order, OrderCancellationRequested> PublishOrderCancellationToWMS(this EventActivityBinder<Order, OrderCancellationRequested> binder)
    {
        return binder.Then(x =>
        {
        });
    }
}
