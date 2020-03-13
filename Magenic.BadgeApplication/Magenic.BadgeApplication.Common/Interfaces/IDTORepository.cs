using Magenic.BadgeApplication.Common.DTO;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Base interface for basic repository classes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDTORepository<T> where T : DTOBase
    {
        /// <summary>
        /// Gets an instance of the DTO
        /// </summary>
        /// <param name="id">The ID of the DTO</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        T Get(int id);

        /// <summary>
        /// Adds the input DTO
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        T Add(T item);

        /// <summary>
        /// Updates the input DTO
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        T Update(T item);

        /// <summary>
        /// Deletes DTO with the input Id
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Deletes Range DTO with the input list of Ids
        /// </summary>
        /// <param name="ids"></param>
        void DeleteRange(IList<int> ids);
    }
}
