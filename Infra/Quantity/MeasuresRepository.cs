using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Quantity
{
    public class MeasuresRepository : UniqueEntityRepository<Measure, MeasureData>, IMeasuresRepository
    { 
        public MeasuresRepository(QuantityDbContext c) : base(c, c.Measures) { }

        

        protected internal override Measure toDomainObject(MeasureData d)=> new Measure(d);
        
        protected internal override IQueryable<MeasureData> addFiltering(IQueryable<MeasureData> set)
        {
            if (string.IsNullOrEmpty(SearchString)) return set;
            return set.Where(s => s.Name.Contains(SearchString)
                                  || s.Code.Contains(SearchString)
                                  || s.Id.Contains(SearchString)
                                  || s.ValidFrom.ToString().Contains(SearchString)
                                  || s.ValidTo.ToString().Contains(SearchString)
                                  || s.Definition.Contains(SearchString)
                                  );
        }
    }
}
