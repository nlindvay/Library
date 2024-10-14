namespace Library.Integration.Tests
{
    using System.Collections.Generic;
    using Library.Components.StateMachines;
    using MassTransit.EntityFrameworkCoreIntegration;
    using Microsoft.EntityFrameworkCore;


    public class IntegrationTestDbContext :
        SagaDbContext
    {
        public IntegrationTestDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override IEnumerable<ISagaClassMap> Configurations => [new ThankYouClassMap(), new BookClassMap(), new ReservationClassMap(), new CheckOutClassMap(), new BookReturnClassMap()];
    }
}