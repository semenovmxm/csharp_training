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
            GroupData group = new GroupData();
            group.Name = "G1 " + DateTime.Now;
            group.Header = "H1 " + DateTime.Now;
            group.Footer = "F1 " + DateTime.Now;
            #endregion

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            #region GroupDataRegion
            GroupData group = new GroupData();
            group.Name = "";
            group.Header = "";
            group.Footer = "";
            #endregion

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            app.Groups.Create(group);
            List<GroupData> newGroups = app.Groups.GetGroupList();
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            #region GroupDataRegion
            GroupData group = new GroupData();
            group.Name = "a'a";
            group.Header = "";
            group.Footer = "";
            #endregion

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            app.Groups.Create(group);
            List<GroupData> newGroups = app.Groups.GetGroupList();
            //Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);

            //verification
            Assert.IsFalse(oldGroups.Count + 1 == newGroups.Count);
        }
    }
}
