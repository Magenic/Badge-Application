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

        private string MessageText
        {
            get { return ConfigurationManager.AppSettings["Message"]; }
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
            var testIndicatorMsg = string.Empty;

            var eventType = string.Empty;
            switch(publishMessageConfig.Environment.ToLower())
            {
                case "prod":
                    eventType = EventType.TeamsEventType.ToString();
                    break;
                default:
                    eventType = EventType.TeamsTestingEventType.ToString();
                    testIndicatorMsg = $"({publishMessageConfig.Environment} test)";
                    break;
            }

            try
            {
                foreach(var item in publishMessageConfig.QueueItems)
                {
                    var formattedMsg = MessageText;
                    if (!string.IsNullOrWhiteSpace(testIndicatorMsg))
                    {
                        formattedMsg = string.Concat(formattedMsg, " ", testIndicatorMsg);
                    }

                    var body = string.Format(formattedMsg,
                        publishMessageConfig.EmployeeFullName,
                        item.BadgeName);

                    var flowMessageRequest = new FlowMessageRequest
                    {
                        eventType = eventType,
                        summary = publishMessageConfig.Title, 
                        body = body,
                        ogImage = item.BadgePath,
                        ogTitle = item.BadgeName,
                        ogDescription = item.BadgeTagline,
                        ogUrl = publishMessageConfig.EmployeeLeaderboard
                    };

                    MakePostRequest(flowMessageRequest, FlowEndpoint);
                    Thread.Sleep(2000);
                }
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
