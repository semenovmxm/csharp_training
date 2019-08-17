using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AurhTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            #region GroupDataRegion
            GroupData newData = new GroupData();
            newData.Name = "G1 " + DateTime.Now;
            newData.Header = "H1 " + DateTime.Now;
            newData.Footer = "F1 " + DateTime.Now;
            #endregion

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(newData);

            Assert.AreEqual(oldGroups.Count +1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(newData);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            #region GroupDataRegion
            GroupData newData = new GroupData();
            newData.Name = "";
            newData.Header = "";
            newData.Footer = "";
            #endregion

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(newData);
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(newData);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            #region GroupDataRegion
            GroupData newData = new GroupData();
            newData.Name = "a'a";
            newData.Header = "";
            newData.Footer = "";
            #endregion

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(newData);
            Assert.IsFalse(oldGroups.Count + 1 == app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(newData);
            oldGroups.Sort();
            newGroups.Sort();
            //Assert.AreEqual(oldGroups, newGroups);
            
            //verification
            Assert.IsFalse(oldGroups == newGroups);
        }
    }
}
