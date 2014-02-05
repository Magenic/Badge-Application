using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;

namespace Magenic.BadgeApplication.BusinessLogic.Tests.Integration
{
    [TestClass]
    public class ActivityControllerTests
    {
        [TestMethod]
        public void TestActivityController()
        {
            TestGet();
        }

        private void TestGet()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string result = client.DownloadString("http://localhost:58603/api/activity");
                }
            }
            catch (Exception ex)
            { 
                
            }
        }
    }
}
