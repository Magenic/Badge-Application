using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass]
    public class SubmitActivityTests
    {
        [TestMethod]
        public async Task GetActivitySubmissionById()
        {
            var submitActivity = await SubmitActivity.GetActivitySubmissionByIdAsync(1);

            Assert.IsNotNull(submitActivity);
            Assert.AreEqual(1, submitActivity.Id);
        }

        [TestMethod]
        public async Task UpdateActivitySubmission()
        {
            var newNotes = "Test Notes";
            var submitActivity = await SubmitActivity.GetActivitySubmissionByIdAsync(1);
            var oldNotes = submitActivity.Notes;
            submitActivity.Notes = newNotes;

            submitActivity = (ISubmitActivity)submitActivity.Save();

            Assert.AreEqual(newNotes, submitActivity.Notes);

            //reset
            submitActivity.Notes = oldNotes;
            submitActivity.Save();
        }

        [TestMethod]
        public void InsertActivitySubmission()
        {
            var newNotes = "Test Notes";
            var aDName = "kevinf";
            var submitActivity = CreateSubmitActivity(newNotes, aDName);

            Assert.IsNotNull(submitActivity);
            Assert.AreEqual(newNotes, submitActivity.Notes);
            Assert.AreEqual(aDName, submitActivity.UserName);
            Assert.AreEqual(ActivitySubmissionStatus.Proposed , submitActivity.Status);
            Assert.IsTrue(submitActivity.Id > 0);
        }

        private ISubmitActivity CreateSubmitActivity(string newNotes, string aDName)
        {
            var submitActivity = SubmitActivity.CreateActivitySubmission(aDName);
            submitActivity.Notes = newNotes;
            submitActivity.ActivityId = 1;

            submitActivity = (ISubmitActivity)submitActivity.Save();
            return submitActivity;
        }

        [TestMethod]
        [ExpectedException(typeof(Csla.DataPortalException))]
        public async Task DeleteSubmitActivity()
        {
            var newNotes = "Test Notes";
            var aDName = "kevinf";
            var submitActivity = CreateSubmitActivity(newNotes, aDName);

            var id = submitActivity.Id;
            submitActivity.Delete();
            submitActivity.Save();

            submitActivity = ((ISubmitActivity)await SubmitActivity.GetActivitySubmissionByIdAsync(id));

            Assert.Fail("Submit Activity should not return.");
        }
    }
}
