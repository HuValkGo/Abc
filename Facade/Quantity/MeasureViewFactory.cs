using System;
using System.Collections.Generic;
using System.Text;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;

namespace Abc.Facade.Quantity
{
    public static class MeasureViewFactory
    {
        public static Measure Create(MeasureView v)
        {
            var d = new MeasureData()
            {
                Id = v.Id,
                Name = v.Name,
                Code = v.Code,
                Definition = v.Definition,
                ValidForm = v.ValidForm,
                ValidTo = v.ValidTo
            };
            return new Measure(d);
        }
        public static MeasureView Create(Measure o)
        {
            var v = new MeasureView
            {
                Id = o.Data.Id,
                Name = o.Data.Name,
                Code = o.Data.Code,
                Definition = o.Data.Definition,
                ValidForm = o.Data.ValidForm,
                ValidTo = o.Data.ValidTo
            };
            return v;
        }
    }
}
