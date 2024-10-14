namespace Library.Integration.Tests
{
    using System;
    using System.Threading.Tasks;
    using Components.StateMachines;
    using Contracts;
    using MassTransit;
    using MassTransit.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;


    public class When_a_book_is_added
    {
        [Test]
        public async Task Should_be_available()
        {
            await using var provider = new ServiceCollection()
                .ConfigureMassTransit(x =>
                {
                    x.AddSagaStateMachine<BookStateMachine, Book>();
                })
                .BuildServiceProvider(true);

            var harness = provider.GetTestHarness();

            await harness.Start();

            var sagaHarness = harness.GetSagaStateMachineHarness<BookStateMachine, Book>();

            var bookId = NewId.NextGuid();
            var memberId = NewId.NextGuid();

            await harness.Bus.Publish<BookAdded>(new
            {
                BookId = bookId,
                Isbn = "0307969959",
                Title = "Neuromancer",
                InVar.Timestamp
            });

            var repository = provider.GetRequiredService<ISagaRepository<Book>>();

            Guid? existsId = await repository.ShouldContainSagaInState(bookId, sagaHarness.StateMachine, x => x.Available, harness.TestTimeout);
            Assert.IsTrue(existsId.HasValue, "Saga was not created using the MessageId");
        }
    }
}