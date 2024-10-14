namespace Library.Contracts
{
    using System;


    public interface ThankYouStatus
    {
        Guid MemberId { get; }
        Guid BookInstanceId { get; }
        string Status { get; }
    }
}