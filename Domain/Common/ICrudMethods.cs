using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abc.Domain.Common
{
    public interface ICrudMethods<TDomain>
    {
        Task<List<TDomain>> Get();
        Task<TDomain> Get(string id);
        Task Delete(string id);
        Task Add(TDomain obj);
        Task Update(TDomain obj);
    }
}