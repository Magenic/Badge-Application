namespace Magenic.BadgeApplication.Teams.Messages
{
    public class FlowMessageRequest : Message
    {
        public string eventType { get; set; }

        public string summary { get; set; }

        public string body { get; set; }

        public string ogImage { get; set; }

        public string ogTitle { get; set; }

        public string ogDescription { get; set; }

        public string ogUrl { get; set; }
    }
}
