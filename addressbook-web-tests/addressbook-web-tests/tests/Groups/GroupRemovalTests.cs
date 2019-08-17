using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AurhTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            #region GroupDataRegion
            GroupData groupToRemove = new GroupData();
            groupToRemove.Index = "0";
            #endregion

            app.Groups.IfExistAnyGroup();

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Remove(groupToRemove);
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();

            GroupData toBeRemoved = oldGroups[int.Parse(groupToRemove.Index)];

            oldGroups.RemoveAt(int.Parse(groupToRemove.Index));
            Assert.AreEqual(oldGroups, newGroups);

            foreach(GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }

        
    }
}
