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
        
     
    }
}
