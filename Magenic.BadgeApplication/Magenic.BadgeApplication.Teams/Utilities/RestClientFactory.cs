using Magenic.BadgeApplication.Common.Interfaces;
using RestSharp;
using System;

namespace Magenic.BadgeApplication.Teams.Utilities
{
    public class RestClientFactory : IRestClientFactory
    {
        public IRestClient Create(Uri url)
        {
            return new RestClient(url);
        }
    }
}
