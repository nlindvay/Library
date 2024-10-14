namespace Library.Contracts
{
    using System;


    public interface BookAdded
    {
        Guid BookInstanceId { get; }

        DateTime Timestamp { get; }

        Guid BookId { get; set; }
        string Isbn { get; }
        string Title { get; }
    }
}