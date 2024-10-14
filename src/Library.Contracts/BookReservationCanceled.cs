namespace Library.Contracts
{
    using System;


    public interface BookReservationCanceled
    {
        Guid BookInstanceId { get; }

        Guid ReservationId { get; }
    }
}