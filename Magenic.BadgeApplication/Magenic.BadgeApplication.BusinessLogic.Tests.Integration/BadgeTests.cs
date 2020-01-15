using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass, Ignore]
    public class BadgeTests : TransactionalTest
    {
        [TestMethod]
        public async Task GetBadgeEditById()
        {
            var activityEdit = await BadgeEdit.GetBadgeEditByIdAsync(1);

            Assert.IsNotNull(activityEdit);
            Assert.AreEqual(1, activityEdit.Id);
        }

        [TestMethod]
        public async Task UpdateBadge()
        {
            var newName = "TestName";
            var badgeEdit = await BadgeEdit.GetBadgeEditByIdAsync(1);
            var oldName = badgeEdit.Name;
            badgeEdit.Name = newName;

            using (var fileStream = System.IO.File.OpenRead(@"..\..\1537400.jpg"))
            {
                var buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int) (fileStream.Length - 1));
                badgeEdit.SetBadgeImage(buffer);
            }

            badgeEdit = (IBadgeEdit) badgeEdit.Save();

            Assert.AreEqual(newName, badgeEdit.Name);

            //reset
            badgeEdit.Name = oldName;
            badgeEdit.Save();
        }

        [TestMethod]
        public void InsertBadge()
        {
            const string newName = "Test Name";
            const string newDescription = "Test Description";
            var badgeEdit = CreateBadgeEdit(newName, newDescription);

            Assert.IsNotNull(badgeEdit);
            Assert.AreEqual(newName, badgeEdit.Name);
            Assert.AreEqual(newDescription, badgeEdit.Description);
            Assert.IsTrue(badgeEdit.Id > 0);
        }

        private IBadgeEdit CreateBadgeEdit(string newName, string newDescription)
        {
            var badgeEdit = BadgeEdit.CreateBadge();
            badgeEdit.Name = newName;
            badgeEdit.Description = newDescription;
            badgeEdit.Tagline = "dsfds";
            badgeEdit.Priority = 5;

            var badgeActivity = new BadgeActivityEdit();
            badgeActivity.ActivityId = 1;
            badgeEdit.BadgeActivities.Add(badgeActivity);

            badgeEdit = (IBadgeEdit) badgeEdit.Save();
            return badgeEdit;
        }

        [TestMethod]
        [ExpectedException(typeof(Csla.DataPortalException))]
        public async Task DeleteBadge()
        {
            const string newName = "Test Name";
            const string newDescription = "Test Description";

            var badgeEdit = CreateBadgeEdit(newName, newDescription);

            var id = badgeEdit.Id;
            badgeEdit.Delete();
            badgeEdit.Save();

            badgeEdit = ((IBadgeEdit)await BadgeEdit.GetBadgeEditByIdAsync(id));

            Assert.Fail("Badge Edit Fail should not return.");

        }

    }
}
