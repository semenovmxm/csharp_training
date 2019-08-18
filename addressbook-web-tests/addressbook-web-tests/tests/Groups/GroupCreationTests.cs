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
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();

            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                    {
                    Header = GenerateRandomString(100), 
                    Footer = GenerateRandomString(100)
                });
            }
            return groups;
        }

        
        [Test, TestCaseSource("RandomGroupDataProvider")]
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(oldGroups.Count +1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
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
