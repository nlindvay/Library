using System;
using System.Threading.Tasks;
using Library.Contracts;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Library.Components.StateMachines;

namespace Library.Components.Tests;

public class When_an_order_is_added
{
    [Test]
    public async Task Should_create_a_saga_instance()
    {
        await using var provider = new ServiceCollection()
            .ConfigureMassTransit(x =>
            {
                x.AddSagaStateMachine<OrderStateMachine, Order>();
            })
            .BuildServiceProvider(true);

        var harness = provider.GetTestHarness();

        await harness.Start();

        var orderId = NewId.NextGuid();

        await harness.Bus.Publish<OrderAdded>(new
        {
            OrderId = orderId,
            PrimaryReference = "0307969959",
            SecondaryReference = "Neuromancer",
            RequiredBy = DateTime.UtcNow
        });

        Assert.IsTrue(await harness.Consumed.Any<OrderAdded>(), "Message not consumed");

        var sagaHarness = harness.GetSagaStateMachineHarness<OrderStateMachine, Order>();

        Assert.IsTrue(await sagaHarness.Consumed.Any<OrderAdded>(), "Message not consumed by saga");

        Assert.That(await sagaHarness.Created.Any(x => x.CorrelationId == orderId));

        var instance = sagaHarness.Created.ContainsInState(orderId, sagaHarness.StateMachine, sagaHarness.StateMachine.Entered);
        Assert.IsNotNull(instance, "Saga instance not found");

        Assert.AreEqual(1, instance.Events.Count);

        Guid? existsId = await sagaHarness.Exists(orderId, x => x.Entered);
        Assert.IsTrue(existsId.HasValue, "Saga did not exist");
    }
}