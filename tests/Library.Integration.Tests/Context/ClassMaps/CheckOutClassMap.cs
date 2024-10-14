namespace Library.Integration.Tests
{
    using Components.StateMachines;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public class CheckOutClassMap :
        SagaClassMap<CheckOut>
    {
        protected override void Configure(EntityTypeBuilder<CheckOut> entity, ModelBuilder model)
        {
            entity.HasIndex(x => new
            {
                x.BookId,
                x.MemberId
            }).IsUnique();
        }
    }
}