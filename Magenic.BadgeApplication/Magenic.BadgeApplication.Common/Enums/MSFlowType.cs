namespace Magenic.BadgeApplication.Common.Enums
{
    /// <summary>
    /// A placeholder enum for event types.
    /// </summary>
    public enum MSFlowType
    {
        /// <summary>
        /// A placeholder value that indicates an event for Microsoft Teams notification for testing.
        /// </summary>
        TeamsTestingEventType = 0,
        /// <summary>
        /// A placeholder value that indicates an event for Outlook notification using the configured Incoming Webhook.
        /// </summary>
        OutlookEventType = 1,
        /// <summary>
        /// A placeholder value that indicates an event for Outlook notification using the Send an Email Flow action.
        /// </summary>
        OutlookEventType2 = 2,
        /// <summary>
        /// A placeholder value that indicates an event for Microsoft Teams notification for production.
        /// </summary>
        TeamsEventType = 3
    }
}