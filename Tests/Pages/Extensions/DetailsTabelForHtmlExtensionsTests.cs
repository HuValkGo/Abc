using System;
using System.Collections.Generic;
using System.Text;
using Abc.Pages.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Extensions
{
    [TestClass]
    public class DetailsTableForHtmlExtensionTests:BaseTests 
    {
        [TestInitialize]
        public virtual void TestInitialize()
        {
            type = typeof(DetailsTableForHtmlExtension);
        }

        [TestMethod]
        public void DetailsTableForTest()
        {
            Assert.Inconclusive();
        }
    }
}
