using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Magenic.BadgeApplication.Common.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class ArgTests
    {
        /// <summary>
        /// Tests the method1.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Can_Get_Arg_Null_Exception()
        {
            object item = null;
            Arg.IsNotNull(() => item);
        }
    }
}
