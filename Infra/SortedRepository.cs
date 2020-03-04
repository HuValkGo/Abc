using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using Abc.Data.Common;
using Abc.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra
{
    public abstract class SortedRepository<TDomain, TData>:BaseRepository<TDomain, TData>, ISorting
        where TData : PeriodData, new()
        where TDomain : Entity<TData>, new()
    {
        public string SortOrder { get; set; }
        public string DescendingString => "_desc";

        protected SortedRepository(DbContext c, DbSet<TData> s) : base(c, s)
        {
        }

        protected internal IQueryable<TData> setSorting(IQueryable<TData> data)
        {
            var expression = createExpression();
            if (expression is null) return data;
            return setOrderBy(data, expression);
        }

       internal Expression<Func<TData,object>> createExpression()
        {
            var property = findProperty();
            if (property is null) return null;
            return lambdaExpression(property);
        }

        internal Expression<Func<TData, object>> lambdaExpression(PropertyInfo p)
        {
            var param = Expression.Parameter(typeof(TData));
            var property = Expression.Property(param, p);
            var body = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func < TData, object >> (body, param);
        }

        internal PropertyInfo findProperty()
        {
            var name = getName();
            return typeof(TData).GetProperty(name);
        }

        private string getName()
        {
            if (string.IsNullOrEmpty(SortOrder)) return string.Empty;
            var idx = SortOrder.IndexOf(DescendingString, StringComparison.Ordinal);
            if(idx > 0) return SortOrder.Remove(idx);
            return SortOrder;
        }

        internal IQueryable<TData>setOrderBy(IQueryable<TData> data, Expression<Func<TData,object>> e)
        {
            if (isDecending()) return data.OrderByDescending(e);
            return data.OrderBy(e);
        }

         internal bool isDecending()
        {
            return SortOrder.EndsWith(DescendingString);
        }
    }
}