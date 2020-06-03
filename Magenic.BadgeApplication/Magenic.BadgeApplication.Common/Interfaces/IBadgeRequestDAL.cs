using Magenic.BadgeApplication.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    public interface IBadgeRequestDAL : IDTORepository<BadgeRequestDTO>
    {
    }
}
