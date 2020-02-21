using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abc.Domain.Quantity;

namespace Abc.Infra.Quantity
{
    public class MeasuresRepository : IMeasuresRepository
    {
        public Task<List<Measure>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Measure> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task Add(Measure obj)
        {
            throw new NotImplementedException();
        }

        public Task Update(Measure obj)
        {
            throw new NotImplementedException();
        }
    }
}
