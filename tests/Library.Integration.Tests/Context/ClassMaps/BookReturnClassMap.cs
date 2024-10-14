namespace Library.Integration.Tests
{
    using Components.StateMachines;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public class BookReturnClassMap :
        SagaClassMap<BookReturn>
    {
        protected override void Configure(EntityTypeBuilder<BookReturn> entity, ModelBuilder model)
        {
            entity.HasIndex(x => new
            {
                x.BookId,
                x.MemberId
            }).IsUnique();
        }
    }
}