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
            group.Index = "1";
            #endregion

            app.Groups.Remove(group);            
        }

        
    }
}
