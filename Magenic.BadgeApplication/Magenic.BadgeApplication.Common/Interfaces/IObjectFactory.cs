using Csla;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// An object factory that represents a CSLA data portal.
    /// </summary>
    /// <typeparam name="T">The type of CSLA object that the data portal is to use.</typeparam>
    public interface IObjectFactory<T> : IDataPortal<T>
    {
        /// <summary>
        /// Creates a new child object.
        /// </summary>
        /// <typeparam name="TC">The type of object to create.</typeparam>
        /// <returns>The created object.</returns>
        TC CreateChild<TC>();
        /// <summary>
        /// Creates a new child object.
        /// </summary>
        /// <typeparam name="TC">The type of object to create.</typeparam>
        /// <param name="parameters">A list of parameters to use in the creation of the object.</param>
        /// <returns>The created object.</returns>
        TC CreateChild<TC>(params object[] parameters);
        /// <summary>
        /// Fetches a child object.
        /// </summary>
        /// <typeparam name="TC">The type of object to fetch.</typeparam>
        /// <returns>The fetched object.</returns>
        TC FetchChild<TC>();
        /// <summary>
        /// Fetches a child object.
        /// </summary>
        /// <typeparam name="TC">The type of object to fetch.</typeparam>
        /// <param name="parameters">A list of parameters to use when fetching the object.</param>
        /// <returns>The fetched object.</returns>
        TC FetchChild<TC>(params object[] parameters);
        /// <summary>
        /// Updates a child object.
        /// </summary>
        /// <param name="child">The object to update.</param>
        void UpdateChild(object child);
        /// <summary>
        /// Updates a child object.
        /// </summary>
        /// <param name="child">The object to update.</param>
        /// <param name="parameters">A list of parameters to use when updating the child object.</param>
        void UpdateChild(object child, params object[] parameters);
    }
}
