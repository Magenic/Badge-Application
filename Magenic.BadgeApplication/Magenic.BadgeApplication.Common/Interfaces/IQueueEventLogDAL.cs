using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Contract for Queue Event Log data access operations
    /// </summary>
    public interface IQueueEventLogDAL : IDTORepository<QueueEventLogDTO>
    {
    }
}
