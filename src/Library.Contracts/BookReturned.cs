namespace Library.Contracts
{
    using System;


    public interface BookReturned
    {
        Guid CheckOutId { get; }

        DateTime Timestamp { get; }

        Guid MemberId { get; }
        Guid BookInstanceId { get; }

        DateTime CheckOutDate { get; }
        DateTime DueDate { get; }
        DateTime ReturnDate { get; }
    }
}