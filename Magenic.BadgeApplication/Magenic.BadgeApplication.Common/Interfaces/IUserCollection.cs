using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A read-only list of user information.
    /// </summary>
    public interface IUserCollection : IReadOnlyListBase<IUserItem>
    {
    }
}