
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra
{
    public abstract class PaginatedRepository<TDomain, TData>:FilteredRepository<TDomain, TData>,IPaging
        where TData : PeriodData, new()
        where TDomain : Entity<TData>, new()
    {
        public int PageSize { get; set; } = 12;
        public int PageIndex { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        protected PaginatedRepository(DbContext c, DbSet<TData> s) : base(c, s)
        {
        }
    }
}