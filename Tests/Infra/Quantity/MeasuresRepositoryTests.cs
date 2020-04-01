using System;
using System.Collections.Generic;
using System.Text;
using Abc.Aids;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Abc.Infra;
using Abc.Infra.Quantity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Quantity
{
    [TestClass]
    public class MeasuresRepositoryTests:RepositoryTests<MeasuresRepository,Measure,MeasureData>
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();

            var options = new DbContextOptionsBuilder<QuantityDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            var c = new QuantityDbContext(options);
            obj = new MeasuresRepository(c);
        }
        protected override Type getBaseType()
        {
            return typeof(UniqueEntityRepository<Measure, MeasureData>);
        }

        protected override void testGetList()
        {
            Assert.Inconclusive();
        }

        protected override string getId(MeasureData d) => d.Id;

        protected override Measure getObject(MeasureData d) =>new Measure(d);

        protected override void setId(MeasureData d, string id) => d.Id = id;
    }
}
