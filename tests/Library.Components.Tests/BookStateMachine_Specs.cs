namespace Library.Components.Tests
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using MassTransit;
    using MassTransit.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using StateMachines;


    public class When_a_book_is_added
    {
        [Test]
        public async Task Should_create_a_saga_instance()
        {
            await using var provider = new ServiceCollection()
                .ConfigureMassTransit(x =>
                {
                    x.AddSagaStateMachine<BookStateMachine, BookInstance>();
                })
                .BuildServiceProvider(true);

            var harness = provider.GetTestHarness();

            await harness.Start();

            var bookInstanceId = NewId.NextGuid();

            await harness.Bus.Publish<BookAdded>(new
            {
                BookInstanceId = bookInstanceId,
                Isbn = "0307969959",
                Title = "Neuromancer"
            });

            Assert.IsTrue(await harness.Consumed.Any<BookAdded>(), "Message not consumed");

            var sagaHarness = harness.GetSagaStateMachineHarness<BookStateMachine, BookInstance>();

            Assert.IsTrue(await sagaHarness.Consumed.Any<BookAdded>(), "Message not consumed by saga");

            Assert.That(await sagaHarness.Created.Any(x => x.CorrelationId == bookInstanceId));

            var instance = sagaHarness.Created.ContainsInState(bookInstanceId, sagaHarness.StateMachine, sagaHarness.StateMachine.Available);
            Assert.IsNotNull(instance, "Saga instance not found");

            Guid? existsId = await sagaHarness.Exists(bookInstanceId, x => x.Available);
            Assert.IsTrue(existsId.HasValue, "Saga did not exist");
        }
    }


    public class When_a_book_is_checked_out
    {
        [Test]
        public async Task Should_change_state_to_checked_out()
        {
            await using var provider = new ServiceCollection()
                .ConfigureMassTransit(x =>
                {
                    x.AddSagaStateMachine<BookStateMachine, BookInstance>();
                })
                .BuildServiceProvider(true);

            var harness = provider.GetTestHarness();

            await harness.Start();

            var bookInstanceId = NewId.NextGuid();

            await harness.Bus.Publish<BookAdded>(new
            {
                BookInstanceId = bookInstanceId,
                Isbn = "0307969959",
                Title = "Neuromancer"
            });

            var sagaHarness = harness.GetSagaStateMachineHarness<BookStateMachine, BookInstance>();

            Guid? existsId = await sagaHarness.Exists(bookInstanceId, x => x.Available);
            Assert.IsTrue(existsId.HasValue, "Saga did not exist");

            await harness.Bus.Publish<BookCheckedOut>(new
            {
                BookInstanceId = bookInstanceId,
                InVar.Timestamp
            });

            existsId = await sagaHarness.Exists(bookInstanceId, x => x.CheckedOut);
            Assert.IsTrue(existsId.HasValue, "Saga was not checked out");
        }
    }
}