namespace Library.Integration.Tests
{
    using Components.StateMachines;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;


    public class ReservationClassMap :
        SagaClassMap<Reservation>
    {
        protected override void Configure(EntityTypeBuilder<Reservation> entity, ModelBuilder model)
        {
            entity.HasIndex(x => new
            {
                x.BookId,
                x.MemberId
            }).IsUnique();
        }
    }
}