namespace Library.Contracts
{
    using System;


    public interface AddBookToMemberCollection
    {
        Guid BookInstanceId { get; }
        Guid MemberId { get; }
    }
}