namespace Library.Integration.Tests
{
    using Components.StateMachines;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public class BookClassMap :
        SagaClassMap<Book>
    {
        protected override void Configure(EntityTypeBuilder<Book> entity, ModelBuilder model)
        {
            entity.HasIndex(x => new
            {
                x.Isbn,
                x.Title
            }).IsUnique();
        }
    }
}