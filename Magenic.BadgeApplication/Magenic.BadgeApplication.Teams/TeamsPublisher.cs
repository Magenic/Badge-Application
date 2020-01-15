using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Teams.Messages;
using MagenicDataModel;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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

        private string MessageText
        {
            get { return ConfigurationManager.AppSettings["Message"]; }
        }

        private string LeaderboardUrl
        {
            get { return ConfigurationManager.AppSettings["LeaderboardURL"]; }
        }

        private string DataServiceUrl
        {
            get { return ConfigurationManager.AppSettings["ITDataServiceURL"]; }
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

        public void Publish(EarnedBadgeItemDTO earnedBadge)
        {
            var userEmail = $"{earnedBadge.EmployeeADName}@magenic.com";

            try
            {
                // Get the user that earned the badge from IT's service endpoint
                var dataServiceUri = new Uri(DataServiceUrl, UriKind.Absolute);
                var context = new MagenicDataEntities(dataServiceUri)
                {
                    Credentials = CredentialCache.DefaultCredentials
                };
                var employee = context.vwODataEmployees.Where(e => e.EMailAddress == userEmail).FirstOrDefault();

                var leaderboardUrl = string.Format(LeaderboardUrl, earnedBadge.EmployeeADName);

                var body = string.Format(MessageText,
                    employee.EmployeeFullName,
                    earnedBadge.Name);

                var flowMessageRequest = new FlowMessageRequest
                {
                    eventType = EventType.TeamsTestingEventType.ToString(), // TODO: Add logic to handle event type, using MS Teams for now
                    summary = "Badge Award!", // TODO: Think about how to construct summary text
                    body = body,
                    ogImage = earnedBadge.ImagePath,
                    ogTitle = earnedBadge.Name,
                    ogDescription = earnedBadge.Tagline,
                    ogUrl = leaderboardUrl
                };

                //try adding the message
                MakePostRequest(flowMessageRequest, FlowEndpoint);
            }
            catch (Exception)
            {
                // TODO: handle error responses
                throw;
            }
        }

        private void MakePostRequest(Message message, string endpoint, string authHeader = null, string contentType = null)
        {
            var request = new RestRequest(endpoint, Method.POST);
            var json = request.JsonSerializer.Serialize(message);
            request.AddParameter("application/json; charset=utf-8", json,
                ParameterType.RequestBody);

            var response = RetryRestRequest(request, TimeSpan.FromSeconds(1));
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
