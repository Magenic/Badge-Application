using Csla;
using System.Linq;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    public interface IUserPermissionCollection : IReadOnlyListBase<IUserPermissionItem>
    {

        /// <summary>
        /// Sort employee permissions collection
        /// </summary>
        /// <param name="sortBy"></param>
        IOrderedEnumerable<IUserPermissionItem> Sort(string sortBy);
    }
}
