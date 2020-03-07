using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Infra;
using Abc.Infra.Quantity;
using Tests;
using System.Threading.Tasks;
using Abc.Aids;
using Abc.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Abc.Tests.Infra
{
    [TestClass]
    public class SortedRepositoryTests : AbstractClassTest<SortedRepository<Measure, MeasureData>,
        BaseRepository<Measure, MeasureData>>
    {
        private class testClass : SortedRepository<Measure, MeasureData>
        {
            public testClass(DbContext c, DbSet<MeasureData> s) : base(c, s)
            {
            }

            protected override Task<MeasureData> getData(string id)
            {
               throw new System.NotImplementedException();
            }
        }

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            var options = new DbContextOptionsBuilder<QuantityDbContext>().UseInMemoryDatabase("TestDb").Options;
            var c =new QuantityDbContext(options);
            obj =new testClass(c,c.Measures);

        }

        [TestMethod]
        public void SortOrderTest()
        {
            isNullableProperty(()=>obj.SortOrder,x=>obj.SortOrder=x);
        }
        [TestMethod]
        public void DescendingStringTest()
        {
            var propertyName = GetMember.Name<testClass>(x => x.DescendingString);
            isReadOnlyProperty(obj,propertyName,"_desc");
        }
        [TestMethod]
        public void SetSortingTest()
        {
            Assert.Inconclusive();
        }
        [TestMethod]
        public void CreateExpressionTest()
        {
            string s;

            TestCreateExpression(GetMember.Name<MeasureData>(x=>x.ValidFrom));
            TestCreateExpression(GetMember.Name<MeasureData>(x => x.ValidTo));
            TestCreateExpression(GetMember.Name<MeasureData>(x => x.Id));
            TestCreateExpression(GetMember.Name<MeasureData>(x => x.Name));
            TestCreateExpression(GetMember.Name<MeasureData>(x => x.Code));
            TestCreateExpression(GetMember.Name<MeasureData>(x => x.Definition));
            TestCreateExpression(s =GetMember.Name<MeasureData>(x => x.ValidFrom),s+obj.DescendingString);
            TestCreateExpression(s=GetMember.Name<MeasureData>(x => x.ValidTo), s + obj.DescendingString);
            TestCreateExpression(s=GetMember.Name<MeasureData>(x => x.Id), s + obj.DescendingString);
            TestCreateExpression(s=GetMember.Name<MeasureData>(x => x.Name), s + obj.DescendingString);
            TestCreateExpression(s=GetMember.Name<MeasureData>(x => x.Code), s + obj.DescendingString);
            TestCreateExpression(s=GetMember.Name<MeasureData>(x => x.Definition), s + obj.DescendingString);
            TestNullExpression (GetRandom.String()); 
            TestNullExpression(String.Empty );
            TestNullExpression(null);
        }

        private void TestNullExpression(string name)
        {
            obj.SortOrder = name;
            var lambda = obj.createExpression();
            Assert.IsNull(lambda);
        }

        private void TestCreateExpression(string expected,string name=null)
        {
            name ??= expected;
            obj.SortOrder =name;
            var lambda = obj.createExpression();
            Assert.IsNotNull(lambda);
            Assert.IsInstanceOfType(lambda, typeof(Expression<Func<MeasureData, object>>));
            Assert.IsTrue(lambda.ToString().Contains(expected));
        }
        [TestMethod]
        public void LambdaExpressionTest()
        {
            var name = GetMember.Name<MeasureData>(x => x.ValidFrom);
            var property = typeof(MeasureData).GetProperty(name);
            var lambda = obj.lambdaExpression(property);
            Assert.IsNotNull(lambda);
            Assert.IsInstanceOfType(lambda, typeof(Expression<Func<MeasureData, object>>));
            Assert.IsTrue(lambda.ToString().Contains(name));
        }
        [TestMethod]
        public void FindPropertyTest()
        {
            string s;
            void Test(PropertyInfo expected, string sortOrder)
            {
                obj.SortOrder = sortOrder;
                Assert.AreEqual(expected, obj.findProperty());
            }
            Test(null, GetRandom.String());
            Test(null, null);
            Test(null, string.Empty);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Name)), s);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.ValidFrom)), s);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.ValidTo)), s);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Definition)), s);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Code)), s);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Id)), s);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Name)), s+ obj.DescendingString);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.ValidFrom)), s + obj.DescendingString);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.ValidTo )), s + obj.DescendingString);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Definition)), s + obj.DescendingString);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Code)), s + obj.DescendingString);
            Test(typeof(MeasureData).GetProperty(s = GetMember.Name<MeasureData>(x => x.Id)), s + obj.DescendingString);
        }
        [TestMethod]
        public void GetNameTest()
        {
            string s;

            void Test(string expected, string sortOrder)
            {
                obj.SortOrder = sortOrder;
                Assert.AreEqual(expected, obj.getName());
            }
            Test(s=GetRandom.String(), s);
            Test(s=GetRandom.String(),s+obj.DescendingString);
            Test(string.Empty, string.Empty);
            Test(string.Empty, null);

        }
        [TestMethod]
        public void SetOrderByTest()
        {
            void Test(IQueryable<MeasureData> d,Expression<Func<MeasureData,object>> e,string expected)
            {
                obj.SortOrder = GetRandom.String() + obj.DescendingString;
                var set = obj.setOrderBy(d, e);
                Assert.IsNotNull(set);
                Assert.AreNotEqual(d,set);
                Assert.IsTrue(set.Expression.ToString()
                    .Contains($"Abc.Data.Quantity.MeasureData]).OrderByDescending({expected})"));
                obj.SortOrder = GetRandom.String();
                set = obj.setOrderBy(d, e);
                Assert.IsNotNull(set);
                Assert.AreNotEqual(d, set);
                Assert.IsTrue(set.Expression.ToString()
                    .Contains($"Abc.Data.Quantity.MeasureData]).OrderBy({expected})"));

            }

            Assert.IsNull(obj.setOrderBy(null,null));
            IQueryable<MeasureData> data =obj.dbSet;
            Assert.AreEqual(data,obj.setOrderBy(data,null));
            Test(data, x => x.Id, "x => x.Id");
            Test(data, x => x.Code, "x => x.Code");
            Test(data, x => x.Name, "x => x.Name");
            Test(data, x => x.Definition, "x => x.Definition");
            Test(data, x => x.ValidFrom, "x => Convert(x.ValidFrom, Object)");
            Test(data, x => x.ValidTo, "x => Convert(x.ValidTo, Object)");
        }
        [TestMethod]
        public void IsDescendingTest()
        {
            void Test(string sortOrder,bool expected)
            {
                obj.SortOrder = sortOrder;
                Assert.AreEqual( expected,obj.isDecending());
            }

            Test(GetRandom.String(),false);
            Test( GetRandom.String() + obj.DescendingString,true);
           Test(string.Empty,false);
           Test(null, false);
        }
    }
}
    

