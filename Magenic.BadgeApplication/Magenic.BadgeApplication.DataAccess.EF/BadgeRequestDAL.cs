using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Exceptions;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class BadgeRequestDAL : IBadgeRequestDAL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "QueueItem")]
        public BadgeRequestDTO Get(int id)
        {
            using (Entities context = new Entities())
            {
                BadgeRequest item = context
                    .BadgeRequests
                    .SingleOrDefault(i => i.BadgeRequestId == id);

                if (item == null)
                {
                    throw new NotFoundException(string.Format("Badge Request with Id {0} could not be found", id));
                }

                var badgeRequest = new BadgeRequestDTO()
                {
                    BadgeRequestId = item.BadgeRequestId,
                    BadgeName = item.BadgeName,
                    BadgeDescription = item.BadgeDescription,
                    EmployeeId = item.EmployeeId,
                    CreatedDate = item.CreatedDate,
                    NotifySentDate = item.NotifySentDate
                };

                return badgeRequest;
            }
        }

        public BadgeRequestDTO Add(BadgeRequestDTO item)
        {
            throw new NotImplementedException();
        }

        public BadgeRequestDTO Update(BadgeRequestDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "BadgeRequestDTO item parameter is null");
            }

            //throw new NotImplementedException();
            using (Entities context = new Entities())
            {
                BadgeRequest itemToUpdate = new BadgeRequest
                {
                    BadgeRequestId = item.BadgeRequestId,
                    BadgeName = item.BadgeName,
                    BadgeDescription = item.BadgeDescription,
                    EmployeeId = item.EmployeeId,
                    CreatedDate = item.CreatedDate,
                    NotifySentDate = item.NotifySentDate
                };

                context.BadgeRequests.Attach(itemToUpdate);
                context.Entry(itemToUpdate).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();

                return Get(item.BadgeRequestId);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IList<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
