using System;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    public abstract class BaseTest<TClass, TBaseClass> where TClass : new()
    {
        protected TClass obj;
        protected Type type;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            obj=new TClass();
            type = obj.GetType();

        }
        [TestMethod]
        public void CanCreateTest()
        {
            Assert.IsNotNull(obj);
        }

        [TestMethod]
        public void IsInheritedTest()
        {
            Assert.AreEqual(typeof(TBaseClass), type.BaseType);
        }
        [TestMethod]
        public void IsSealed()
        {
            Assert.IsTrue(type.IsSealed);
        }
    }
}