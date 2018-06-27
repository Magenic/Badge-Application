using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Teams.Messages;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Threading;

namespace Magenic.BadgeApplication.Teams
{
    public class TeamsPublisher : IPublisher
    {
        private IContainer _factory;

        private IRestClientFactory _restClientFactory;
        private IRestClient _restClient;

        private string TeamsMessageText
        {
            get { return ConfigurationManager.AppSettings["TeamsMessage"]; }
        }

        private string TeamsWebhookEndpoint
        {
            get { return ConfigurationManager.AppSettings["TeamsWebhookEndpoint"]; }
        }

        public TeamsPublisher() : this(IoC.Container)
        {
        }

        public TeamsPublisher(IContainer factory)
        {
            _factory = factory;

            _restClientFactory = _factory.Resolve<IRestClientFactory>();

            var teamsBaseUrl = ConfigurationManager.AppSettings["TeamsBaseURL"];
            _restClient = _restClientFactory.Create(new Uri(teamsBaseUrl));
        }

        public void Publish(EarnedBadgeItemDTO earnedBadge)
        {
            // TODO: Form user url from Yammer

            try
            {
                // TODO: Get the user that earned the badge using user url

                //let's post a message now to this group
                var broadcastToAll = false;

                var msg = string.Format(TeamsMessageText,
                    "12345", // TODO: Find a way to get the "yammerUser.UserID"
                    earnedBadge.Name,
                    broadcastToAll,
                    "https://badgeapplication.magenic.com/Leaderboard/show/" + earnedBadge.EmployeeADName,
                    earnedBadge.ImagePath,
                    earnedBadge.Name,
                    earnedBadge.Tagline);
                var postMessage = new PostWebhookMessage
                {
                    text = msg
                };

                //try adding the message
                MakePostRequest(postMessage, TeamsWebhookEndpoint);
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
            if (response.StatusCode != HttpStatusCode.OK)
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
