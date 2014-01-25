using System.Runtime.Serialization;

namespace Magenic.BadgeApplication.Yammer.Helpers
{
    [DataContract]
    public class YammerNetworkSettings
    {
        [DataMember(Name = "message_prompt")]
        public string MessagePrompt { get; set; }

        [DataMember(Name = "allow_attachments")]
        public bool AllowAttachments { get; set; }

        [DataMember(Name = "show_communities_directory")]
        public bool ShowCommunitiesDirectory { get; set; }

        [DataMember(Name = "enable_groups")]
        public bool EnableGroups { get; set; }

        [DataMember(Name = "allow_yammer_apps")]
        public bool AllowYammerApps { get; set; }

        [DataMember(Name = "admin_can_delete_messages")]
        public bool AdminCanDeleteMessages { get; set; }

        [DataMember(Name = "allow_inline_document_view")]
        public bool AllowInlineDocumentView { get; set; }

        [DataMember(Name = "allow_inline_video")]
        public bool AllowInlineVideo { get; set; }

        [DataMember(Name = "enable_private_messages")]
        public bool EnablePrivateMessages { get; set; }

        [DataMember(Name = "allow_external_sharing")]
        public bool AllowExternalSharing { get; set; }

        [DataMember(Name = "enable_chat")]
        public bool EnableChat { get; set; }
    }
}
