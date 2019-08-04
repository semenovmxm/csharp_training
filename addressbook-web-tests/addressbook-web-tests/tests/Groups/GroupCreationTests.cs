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

            app.Groups.Create(group);
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

            app.Groups.Create(group);
        }


    }
}
