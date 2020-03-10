using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Teams.Messages
{
    public class AdaptFlowMsg
    {
        public string type { get; set; }
        public List<AdaptFlowMsgBody> body { get; set; }
        public List<AdaptFlowMsgAction> actions { get; set; }
        public string schemaNameToBeReplaced { get; set; }
        public string version { get; set; }
    }
}
