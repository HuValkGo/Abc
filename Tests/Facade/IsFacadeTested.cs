using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade
{
    [TestClass]
    public class IsFacadeTested:AssemblyTests
    {
        private const string assembly = "Abc.Facade";

        protected override string Namespace(string name) { return $"{assembly}.{name}"; }

        [TestMethod] public void IsCommonTested() { isAllTested(assembly, Namespace("Common")); }
        [TestMethod] public void IsQuantityTested() { isAllTested(assembly, Namespace("Quantity")); }
        [TestMethod] public void IsTested() { isAllTested(base.Namespace("Facade")); }
    }
}
