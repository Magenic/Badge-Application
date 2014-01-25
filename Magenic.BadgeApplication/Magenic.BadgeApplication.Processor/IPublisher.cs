using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Processor
{
    public interface IPublisher
    {
        void Publish(EarnedBadgeItemDTO earnedBadge);
    }
}
