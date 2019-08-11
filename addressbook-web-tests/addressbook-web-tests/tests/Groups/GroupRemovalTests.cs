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
            GroupData group = new GroupData();
            group.Index = "0";
            #endregion

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            
            app.Groups.
                IfExistAnyGroup(group).
                Remove(group);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            oldGroups.RemoveAt(int.Parse(group.Index));
            Assert.AreEqual(oldGroups, newGroups);
        }

        
    }
}
