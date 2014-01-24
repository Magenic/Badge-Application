using Magenic.BadgeApplication.BusinessLogic.Activity;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass]
    public class ActivityTests
    {
        [TestInitialize]
        public void TestInit()
        {
            //ApplicationContext.User = new GenericPrincipal(new GenericIdentity("foo", "Csla"), new[] { IntroToCSLA.BusinessObjects.Constants.Values.Admin });
        }

        [TestCleanup]
        public void Teardown()
        {
            //ApplicationContext.User = new GenericPrincipal(new GenericIdentity("foo", "Csla"), new[] { IntroToCSLA.BusinessObjects.Constants.Values.Admin });
        }

        [TestMethod]
        public async Task GetActivityEditById()
        {
            var activityEdit = await ActivityEdit.GetActivityEditByIdAsync(1);

            Assert.IsNotNull(activityEdit);
            Assert.AreEqual(1, activityEdit.Id);
        }

        [TestMethod]
        public async Task UpdateActivity()
        {
            var newName = "TestName";
            var activityEdit = await ActivityEdit.GetActivityEditByIdAsync(1);
            var oldName = activityEdit.Name;
            activityEdit.Name = newName;

            activityEdit = (IActivityEdit)activityEdit.Save();

            Assert.AreEqual(newName, activityEdit.Name);

            //reset
            activityEdit.Name = oldName;
            activityEdit.Save();
        }

        [TestMethod]
        public void InsertActivity()
        {
            var newName = Guid.NewGuid().ToString();
            const string newDescription = "Test Description";
            var activityEdit = ActivityEdit.CreateActivity();
            activityEdit.Name = newName;
            activityEdit.Description = newDescription;

            activityEdit = (IActivityEdit)activityEdit.Save();

            Assert.IsNotNull(activityEdit);
            Assert.AreEqual(newName, activityEdit.Name);
            Assert.AreEqual(newDescription, activityEdit.Description);
            Assert.IsFalse(activityEdit.RequiresApproval);
            Assert.IsTrue(activityEdit.Id > 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Csla.DataPortalException))]
        public async Task DeleteActivity()
        {
            var newName = Guid.NewGuid().ToString();
            const string newDescription = "Test Description";
            var activityEdit = ActivityEdit.CreateActivity();
            activityEdit.Name = newName;
            activityEdit.Description = newDescription;

            activityEdit = (IActivityEdit)activityEdit.Save();

            var id = activityEdit.Id;
            activityEdit.Delete();
            activityEdit.Save();

            activityEdit = ((IActivityEdit) await ActivityEdit.GetActivityEditByIdAsync(id));

            Assert.Fail("Activity Edit Fail should not return.");
        }

        [TestMethod]
        public async Task ActivityNameSameAsExisting()
        {
            var activityEdit = await ActivityEdit.GetActivityEditByIdAsync(1);
            var secondActivity = await ActivityEdit.GetActivityEditByIdAsync(2);
            activityEdit.Name = secondActivity.Name;

            Assert.IsFalse(activityEdit.IsValid);
        }

        [TestMethod]
        public async Task ActivityNameNotSameAsExisting()
        {
            var activityEdit = await ActivityEdit.GetActivityEditByIdAsync(1);
            activityEdit.Name = Guid.NewGuid().ToString();

            Assert.IsTrue(activityEdit.IsValid);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "NameAble"), TestMethod]
        public async Task ActivityNameAbleToBeSetToDbValue()
        {
            var activityEdit = await ActivityEdit.GetActivityEditByIdAsync(1);
            var oldName = activityEdit.Name;
            activityEdit.Name = Guid.NewGuid().ToString();
            activityEdit.Name = oldName;

            Assert.IsTrue(activityEdit.IsValid);
        }

    }
}
