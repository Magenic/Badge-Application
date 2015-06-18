using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// A list of submitted activities that a manager can approve/deny.
    /// </summary>
    public interface IApproveActivityManagerCollection : IBusinessListBase<IApproveActivityItemForManager>
    {
    }
}
