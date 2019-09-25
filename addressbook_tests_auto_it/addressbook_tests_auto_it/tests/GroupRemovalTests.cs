using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace addressbook_tests_auto_it
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void TestGroupRemoval()
        {
            GroupData groupToRemove = new GroupData();

            //app.Groups.IfExistAnyGroup();
            
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData toBeRemoved = oldGroups[0];
            oldGroups.RemoveAt(0);
            app.Groups.Remove(0);
            Assert.AreEqual(oldGroups.Count, Convert.ToInt32(app.Groups.GetGroupCount()));

            List<GroupData> newGroups = app.Groups.GetGroupList();

            
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

        }
    }
}
