using Magenic.BadgeApplication.Yammer.Helpers;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerMessage : SerializedJson<YammerMessage>
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "sender_id")]
        public string SenderID { get; set; }

        [DataMember(Name = "replied_to_id")]
        public string RepliedToID { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }

        [DataMember(Name = "network_id")]
        public string NetworkID { get; set; }

        [DataMember(Name = "message_type")]
        public string MessageType { get; set; }

        [DataMember(Name = "sender_type")]
        public string SenderType { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "web_url")]
        public string WebUrl { get; set; }

        [DataMember(Name = "body")]
        public YammerMessageContent MessageContent { get; set; }

        [DataMember(Name = "thread_id")]
        public string ThreadID { get; set; }

        [DataMember(Name = "client_type")]
        public string ClientType { get; set; }

        [DataMember(Name = "client_url")]
        public string ClientUrl { get; set; }

        [DataMember(Name = "system_message")]
        public bool SystemMessage { get; set; }

        [DataMember(Name = "direct_message")]
        public bool DirectMessage { get; set; }

        [DataMember(Name = "chat_client_sequence")]
        public string ChatClientSequence { get; set; }

        [DataMember(Name = "content_excerpt")]
        public string ContentExcerpt { get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "attachments")]
        public List<YammerAttachment> Attachments { get; set; }

        [DataMember(Name = "liked_by")]
        public YammerLikes Likes { get; set; }

        public YammerMessage()
        {
            this.Attachments = new List<YammerAttachment>();
            this.Likes = new YammerLikes();
            this.MessageContent = new YammerMessageContent();
        }
    }
}
