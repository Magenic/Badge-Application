using Magenic.BadgeApplication.Common.Interfaces;
using RestSharp;
using System;

namespace Magenic.BadgeApplication.Common.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class RestClientFactory : IRestClientFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public IRestClient Create(Uri url)
        {
            return new RestClient(url);
        }
    }
}
