namespace Library.Contracts
{
    using MassTransit;


    public static class MessageContracts
    {
        static bool _initialized;

        public static void Initialize()
        {
            if (_initialized)
                return;

            GlobalTopology.Send.UseCorrelationId<BookAdded>(x => x.BookInstanceId);
            GlobalTopology.Send.UseCorrelationId<BookCheckedOut>(x => x.BookInstanceId);
            GlobalTopology.Send.UseCorrelationId<RenewCheckOut>(x => x.CheckOutId);
            GlobalTopology.Send.UseCorrelationId<BookReservationCanceled>(x => x.BookInstanceId);
            GlobalTopology.Send.UseCorrelationId<ReservationRequested>(x => x.ReservationId);
            GlobalTopology.Send.UseCorrelationId<ReservationCancellationRequested>(x => x.ReservationId);
            GlobalTopology.Send.UseCorrelationId<ReservationExpired>(x => x.ReservationId);

            _initialized = true;
        }
    }
}