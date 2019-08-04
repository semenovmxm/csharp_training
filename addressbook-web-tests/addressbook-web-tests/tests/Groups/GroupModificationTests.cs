using System;
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
            GroupData group = new GroupData();
            group.Index = "1";
            group.Name = "G1 " + DateTime.Now;
            group.Header = null; // "H1 " + DateTime.Now;
            group.Footer = null; // "F1 " + DateTime.Now;
            #endregion

            app.Groups.Modify(group);
        }
    }
}
