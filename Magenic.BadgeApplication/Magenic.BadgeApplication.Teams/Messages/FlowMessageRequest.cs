namespace Magenic.BadgeApplication.Teams.Messages
{
    public class FlowMessageRequest : Message
    {
        public string webhookUrl { get; set; }

        public string messageSubject { get; set; }

        public string messageBody { get; set; }
    }
}
