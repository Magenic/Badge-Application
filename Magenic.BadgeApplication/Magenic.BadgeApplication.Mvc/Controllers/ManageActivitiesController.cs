using Csla.Rules;
using Csla.Web.Mvc;
using Magenic.BadgeApplication.BusinessLogic.Activity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ManageActivitiesController
        : BaseController
    {
        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "jt"), HttpPost]
        [HasPermission(AuthorizationActions.GetObject, typeof(ActivityEditCollection))]
        public async Task<JsonResult> List(int jtStartIndex, int jtPageSize)
        {
            var activities = await ActivityEditCollection.GetAllActivitiesAsync();

            var totalRecourds = activities.Count();
            var pagedActivities = activities.Skip(jtStartIndex).Take(jtPageSize);

            return Json(new { Result = "OK", Records = pagedActivities, TotalRecordCount = totalRecourds });
        }

        /// <summary>
        /// Creates the specified activity edit.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(AuthorizationActions.CreateObject, typeof(ActivityEdit))]
        public async Task<JsonResult> Create()
        {
            var activityEdit = ActivityEdit.CreateActivity();
            TryUpdateModel(activityEdit);
            if (await SaveObjectAsync(activityEdit, true))
            {
                return Json(new { Result = "OK", Record = activityEdit });
            }

            return Json(new { Result = "ERROR", Message = String.Join("<br />", ModelState.Values.SelectMany(ms => ms.Errors).Select(me => me.ErrorMessage)) });
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(AuthorizationActions.EditObject, typeof(ActivityEdit))]
        public async Task<JsonResult> Update(int id)
        {
            var activityEdit = await ActivityEdit.GetActivityEditByIdAsync(id);
            TryUpdateModel(activityEdit);
            if (await SaveObjectAsync(activityEdit, true))
            {
                return Json(new { Result = "OK" });
            }

            return Json(new { Result = "ERROR", Message = String.Join("<br />", ModelState.Values.SelectMany(ms => ms.Errors).Select(me => me.ErrorMessage)) });
        }

        /// <summary>
        /// Deletes the specified activity edit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HasPermission(AuthorizationActions.DeleteObject, typeof(ActivityEdit))]
        public async virtual Task<JsonResult> Delete(int id)
        {
            var activityEdit = await ActivityEdit.GetActivityEditByIdAsync(id);

            activityEdit.Delete();
            await SaveObjectAsync(activityEdit, true);

            return Json(new { Result = "OK" });
        }
    }
}