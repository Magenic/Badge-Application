using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Teams.Messages;
using MagenicDataModel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace Magenic.BadgeApplication.Teams
{
    public class TeamsPublisher : IPublisher
    {
        private IContainer _factory;

        private IRestClientFactory _restClientFactory;
        private IRestClient _restClient;

        private string FlowEndpoint
        {
            get { return ConfigurationManager.AppSettings["FlowEndpoint"]; }
        }

        private string FlowBaseURL
        {
            get { return ConfigurationManager.AppSettings["FlowBaseURL"]; }
        }

        public TeamsPublisher() : this(IoC.Container)
        {
        }

        public TeamsPublisher(IContainer factory)
        {
            _factory = factory;

            _restClientFactory = _factory.Resolve<IRestClientFactory>();

            var flowBaseUrl = ConfigurationManager.AppSettings["FlowBaseURL"];
            _restClient = _restClientFactory.Create(new Uri(flowBaseUrl));
        }

        public void Publish(PublishMessageConfigDTO publishMessageConfig)
        {
            var msgEmp = $"{publishMessageConfig.EmployeeFullName} has been awarded the following Magenic Badges:";

            var eventType = string.Empty;
            switch(publishMessageConfig.Environment.ToLower())
            {
                case "prod":
                    eventType = EventType.TeamsEventType.ToString();
                    break;
                default:
                    eventType = EventType.TeamsTestingEventType.ToString();
                    msgEmp = $"{msgEmp} ({publishMessageConfig.Environment} test)";
                    break;
            }

            try
            {
                var adptMsg = new AdaptFlowMsg();
                adptMsg.type = "AdpativeCard";
                adptMsg.body = new List<AdaptFlowMsgBody>();
                adptMsg.actions = new List<AdaptFlowMsgAction>();
                adptMsg.schemaNameToBeReplaced = "http://adaptivecards.io/schemas/adaptive-card.json";
                adptMsg.version = "1.0";

                var adptMsgAction = new AdaptFlowMsgAction()
                {
                    type = "Action.OpenUrl",
                    title = "View",
                    url = publishMessageConfig.EmployeeLeaderboard
                };
                adptMsg.actions.Add(adptMsgAction);

                //var adptMsgBodyList = new List<AdaptFlowMsgBody>();
                var adptMsgBody = new AdaptFlowMsgBody()
                {
                    type = "Container",
                    items = new List<string>()
                };

                adptMsgBody.items.Add("@TextBlock");

                var textBlock = new AdaptFlowMsgTextBlock()
                {
                    type = "TextBlock",
                    size = "Medium",
                    weight = "Bold",
                    text = "This is Person"
                };

                var columnSets = new List<AdaptFlowMsgColumnSet>();
                var column1s = new List<AdaptFlowMsgColumn>();
                var column1Images = new List<AdaptFlowMsgImage>();
                var column2s = new List<AdaptFlowMsgColumn>();
                var column2TextBlocks = new List<AdaptFlowMsgTextBlock>();
                var badgeCnt = 0;
                foreach (var qItem in publishMessageConfig.QueueItems)
                {
                    badgeCnt++;

                    adptMsgBody.items.Add($"@ColumnSet{badgeCnt.ToString()}");
                    var columnSet = new AdaptFlowMsgColumnSet()
                    {
                        type = "ColumnSet",
                        columns = new List<string>()
                    };
                    columnSets.Add(columnSet);

                    columnSet.columns.Add($"@Column1Set{badgeCnt.ToString()}");
                    var column1 = new AdaptFlowMsgColumn()
                    {
                        type = "Column",
                        items = new List<string>(),
                        width = "auto"
                    };
                    column1s.Add(column1);

                    column1.items.Add($"@ItemColumn1Set{badgeCnt.ToString()}");
                    var itemColumn1 = new AdaptFlowMsgImage()
                    {
                        type = "Image",
                        style = "Person",
                        url = "https://magenicbadgeappprod.blob.core.windows.net/badgeimagesqa/badgeimage36",
                        size = "Small"
                    };
                    column1Images.Add(itemColumn1);

                    columnSet.columns.Add($"@Column2Set{badgeCnt.ToString()}");
                    var column2 = new AdaptFlowMsgColumn()
                    {
                        type = "Column",
                        items = new List<string>(),
                        width = "auto"
                    };
                    column2s.Add(column2);

                    column2.items.Add($"@ItemColumn2Set{badgeCnt.ToString()}");
                    var itemColumn2 = new AdaptFlowMsgTextBlock()
                    {
                        type = "TextBlock",
                        size = "Medium",
                        weight = "Normal",
                        text = "This is Badge Name - Badge Tagline"
                    };
                    column2TextBlocks.Add(itemColumn2);
                }

                adptMsg.body.Add(adptMsgBody);

                var adptJson = JsonConvert.SerializeObject(adptMsg);
                adptJson = adptJson.Replace("schemaNameToBeReplaced", "$schema");
                var textBlockJson = JsonConvert.SerializeObject(textBlock);
                adptJson = adptJson.Replace("@TextBlock", textBlockJson);

                for (int i = 0; badgeCnt > 0 && i < badgeCnt; i++)
                {
                    var columnSetJson = JsonConvert.SerializeObject(columnSets[i]);
                    adptJson = adptJson.Replace($"\"@ColumnSet{(i + 1).ToString()}\"", columnSetJson);

                    var column1Json = JsonConvert.SerializeObject(column1s[i]);
                    adptJson = adptJson.Replace($"\"@Column1Set{(i + 1).ToString()}\"", column1Json);

                    var itemColumn1SetJson = JsonConvert.SerializeObject(column1Images[i]);
                    adptJson = adptJson.Replace($"\"@ItemColumn1Set{(i + 1).ToString()}\"", itemColumn1SetJson);

                    var column2Json = JsonConvert.SerializeObject(column2s[i]);
                    adptJson = adptJson.Replace($"\"@Column2Set{(i + 1).ToString()}\"", column2Json);

                    var itemColumn2Json = JsonConvert.SerializeObject(column2TextBlocks[i]);
                    adptJson = adptJson.Replace($"\"@ItemColumn2Set{(i + 1).ToString()}\"", itemColumn2Json);
                }

                //StringBuilder sb = new StringBuilder();
                //sb.Append($"{msgEmp}<br/>");
                //sb.Append("<table>");
                //foreach (var item in publishMessageConfig.QueueItems)
                //{
                //    sb.Append("<tr>");
                //    sb.Append($"<td><img src='{item.BadgePath}' width='50%' height='50%' alt='{item.BadgeName}' /></td>");
                //    sb.Append($"<td>&nbsp;{item.BadgeName}</td>");
                //    sb.Append("<td>");
                //    if (!string.IsNullOrWhiteSpace(item.BadgeTagline))
                //    {
                //        sb.Append($" - {item.BadgeTagline}</td>");
                //    }
                //    sb.Append("</td>");
                //    sb.Append("</tr>");
                //}
                //sb.Append("</table>");
                //sb.Append("<p>");

                //var msgBody = sb.ToString();

                var flowMessageRequest = new FlowMessageRequest
                {
                    eventType = "TempFlow",
                    //summary = publishMessageConfig.Title, // TODO: Think about how to construct summary text
                    body = adptJson,
                    //ogImage = string.Empty,
                    //ogTitle = "Badge Name",
                    //ogDescription = "Badge Tag Line",
                    //ogUrl = publishMessageConfig.EmployeeLeaderboard
                };

                MakePostRequest(flowMessageRequest, FlowEndpoint);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void MakePostRequest(Message message, string endpoint, string authHeader = null, string contentType = null)
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            var request = new RestRequest(endpoint, Method.POST);
            var json = request.JsonSerializer.Serialize(message);
            request.AddParameter("application/json; charset=utf-8", json,
                ParameterType.RequestBody);

            Logger.InfoFormat<TeamsPublisher>("Teams JSON: {0}", json);

            var response = RetryRestRequest(request, TimeSpan.FromSeconds(1));

            Logger.InfoFormat<TeamsPublisher>("Teams publisher published with response status {0} to endpoint {1}", response.StatusCode, endpoint);

            if ((int)response.StatusCode < 200 || (int)response.StatusCode > 206)
            {
                throw new Exception("Error in POST");
            }
        }

        private IRestResponse RetryRestRequest(IRestRequest restRequest, TimeSpan retryInterval, int retryCount = 3)
        {
            IRestResponse restResponse = new RestResponse();
            Retry<object>(() =>
            {
                var response = _restClient.Execute(restRequest);
                restResponse = response;
                return response;
            }, retryInterval, retryCount);

            return restResponse;
        }

        private static R Retry<R>(Func<R> action, TimeSpan retryInterval, int retryCount = 3)
        {
            var exceptions = new List<Exception>();

            for (int retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    if (retry > 0)
                        Thread.Sleep(retryInterval);
                    return action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            throw new AggregateException(exceptions);
        }
    }
}
