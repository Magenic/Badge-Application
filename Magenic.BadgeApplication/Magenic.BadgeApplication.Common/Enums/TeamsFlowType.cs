namespace Magenic.BadgeApplication.Common.Enums
{
    /// <summary>
    /// A placeholder enum for event types.
    /// </summary>
    public enum TeamsFlowType
    {
        /// <summary>
        /// A placeholder value that indicates an event for Microsoft Teams notification for testing.
        /// </summary>
        TestingType = 0,
        /// <summary>
        /// A placeholder value that indicates an event for Outlook notification using the configured Incoming Webhook.
        /// </summary>
        OutlookType = 1,
        /// <summary>
        /// A placeholder value that indicates an event for Outlook notification using the Send an Email Flow action.
        /// </summary>
        OutlookType2 = 2,
        /// <summary>
        /// A placeholder value that indicates an event for Microsoft Teams notification for production.
        /// </summary>
        TeamsType = 3
    }
}