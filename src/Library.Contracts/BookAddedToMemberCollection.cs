namespace Library.Contracts
{
    using System;


    public interface BookAddedToMemberCollection
    {
        Guid BookInstanceId { get; }
        Guid MemberId { get; }
    }
}