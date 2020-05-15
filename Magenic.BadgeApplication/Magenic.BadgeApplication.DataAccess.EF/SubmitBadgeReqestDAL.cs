using System;
using System.Threading.Tasks;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class SubmitBadgeReqestDAL : ISubmitBadgeRequestDAL
    {
        public void Delete(int badgeRequestSubmissionId)
        {
            throw new NotImplementedException();
        }

        public Task<SubmitBadgeRequestDTO> GetBadgeRequestSubmissionByIdAsync(int badgeRequestId)
        {
            throw new NotImplementedException();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public SubmitBadgeRequestDTO Insert(SubmitBadgeRequestDTO data)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var saveBadgeRequestSubmission = LoadData(data);
                ctx.BadgeRequests.Add(saveBadgeRequestSubmission);

                ctx.SaveChanges();
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }
                data.BadgeRequestId = saveBadgeRequestSubmission.BadgeRequestId;
            }
            return data;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public SubmitBadgeRequestDTO Update(SubmitBadgeRequestDTO data)
        {
            throw new NotImplementedException();
        }

        private static BadgeRequest LoadData(SubmitBadgeRequestDTO data)
        {
            var badgeRequest = new BadgeRequest
            {
                BadgeRequestId = data.BadgeRequestId,
                BadgeDescription = data.Description,
                BadgeName = data.Name,
                EmployeeId = data.EmployeeId
            };
            return badgeRequest;
        }
    }
}
