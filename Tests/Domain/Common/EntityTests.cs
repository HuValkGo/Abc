using System;
using System.Collections.Generic;
using System.Text;
using Abc.Aids;
using Abc.Data.Common;
using Abc.Data.Quantity;
using Abc.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Common
{
    [TestClass]
    public class EntityTests :AbstractClassTests<Entity<MeasureData>,object>
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            obj = new testClass();
        }
        private class testClass : Entity<MeasureData>
        {
            public testClass(MeasureData d=null) : base(d) { }
        }

        [TestMethod]
        public void DataTest()
        {
            var d = GetRandom.Object<MeasureData>();
            Assert.AreNotSame(d,obj.Data);
            obj = new testClass(d);
            Assert.AreSame(d, obj.Data);

        }
        [TestMethod]
        public void DataIsNullTest()
        {
            var d = GetRandom.Object<MeasureData>();
            Assert.IsNotNull(obj.Data);
            obj.Data = d;
            Assert.AreSame(d, obj.Data);
        }
        [TestMethod]
        public void CanSetNullDataTest()
        {
            var d = GetRandom.Object<MeasureData>();
           obj =new testClass(d);
            Assert.IsNotNull(obj.Data);
            obj.Data = null;
            Assert.IsNull(obj.Data);
        }
    }
}
