﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AurhTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            #region GroupDataRegion
            GroupData newData = new GroupData();
            newData.Index = "0";
            newData.Name = "G1 " + DateTime.Now;
            newData.Header = null; // "H1 " + DateTime.Now;
            newData.Footer = null; // "F1 " + DateTime.Now;
            #endregion


            app.Groups.IfExistAnyGroup();

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData oldData = oldGroups[0];

            app.Groups.Modify(newData);
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if(group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
