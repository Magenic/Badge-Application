using RestSharp;
using System;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Defines the contract for creating instances of RestClient.
    /// </summary>
    public interface IRestClientFactory
    {
        /// <summary>
        /// Creates a RestClient with the specified base URL.
        /// </summary>
        /// <param name="url">The base URL.</param>
        /// <returns></returns>
        IRestClient Create(Uri url);
    }
}
