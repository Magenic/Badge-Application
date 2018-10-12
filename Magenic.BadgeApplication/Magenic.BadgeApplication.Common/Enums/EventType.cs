namespace Magenic.BadgeApplication.Common.Enums
{
    /// <summary>
    /// A placeholder enum for event types.
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// A placeholder value that indicates an event for Microsoft Teams notification.
        /// </summary>
        TeamsEventType = 0,
        /// <summary>
        /// A placeholder value that indicates an event for Outlook notification using the configured Incoming Webhook.
        /// </summary>
        OutlookEventType = 1,
        /// <summary>
        /// A placeholder value that indicates an event for Outlook notification using the Send an Email Flow action.
        /// </summary>
        OutlookEventType2 = 2
    }
}