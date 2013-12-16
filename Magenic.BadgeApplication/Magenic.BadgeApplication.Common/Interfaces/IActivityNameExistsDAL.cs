using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Checks to see if a given activity name already exists.
    /// </summary>
    public interface IActivityNameExistsDAL
    {
        /// <summary>
        /// Takes a name and returns if it already exists.
        /// </summary>
        /// <param name="name">The name to check for.</param>
        /// <returns>A <see cref="bool"/> indicating if the name already exists.</returns>
        Task<bool> ActivityNameExistsAsync(string name);
    }
}
