using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Quantity
{
    public class MeasuresRepository : IMeasuresRepository
    {
        protected internal QuantityDbContext Db;
        public string SortOrder { get; set; }
        public string SearchString { get; set; }
        public int PageSize { get; set; } = 1;
        public int PageIndex { get; set; } = 1;
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }

        public MeasuresRepository(QuantityDbContext c)
        {
            Db = c;
        }

        public async Task<List<Measure>> Get()
        {
            var list = await CreatePaged(CreateFiltered(CreateSorted()));
            HasNextPage = list.HasNextPage;
            HasPreviousPage = list.HasPreviousPage;
            return list.Select(e => new Measure(e)).ToList();

        }

        private async Task<PaginatedList<MeasureData>>CreatePaged(IQueryable<MeasureData> dataSet)
        {
            return await PaginatedList<MeasureData>.CreateAsync(
                dataSet, PageIndex, PageSize);
        }

        private IQueryable<MeasureData> CreateFiltered(IQueryable<MeasureData> set)
        {
            if (String.IsNullOrEmpty(SearchString)) return set;
            return set.Where(s => s.Name.Contains(SearchString)
                                  || s.Code.Contains(SearchString)
                                  || s.Id.Contains(SearchString)
                                  || s.ValidForm.ToString().Contains(SearchString)
                                  || s.ValidTo.ToString().Contains(SearchString)
                                  || s.Definition.Contains(SearchString)
                                  );
        }

        private IQueryable<MeasureData> CreateSorted()
        {
            IQueryable<MeasureData> measures= from s in Db.Measures select s;

            switch (SortOrder)
            {
                case "name_desc":
                    measures = measures.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    measures = measures.OrderBy(s => s.ValidForm);
                    break;
                case "date_desc":
                    measures = measures.OrderByDescending(s => s.ValidForm);
                    break;
                default:
                    measures = measures.OrderBy(s => s.Name);
                    break;
            }

            return measures.AsNoTracking();
        }
        
        public async Task<Measure> Get(string id)
        {
            var d = await Db.Measures.FirstOrDefaultAsync(m => m.Id == id);
            return new Measure(d);
        }

        public async Task Delete(string id)
        {
            var d = await Db.Measures.FindAsync(id);

            if (d is null) return;

            Db.Measures.Remove(d);
            await Db.SaveChangesAsync();
        }

        public async Task Add(Measure obj)
        {
            Db.Measures.Add(obj.Data);
            await Db.SaveChangesAsync();
        }

        public async Task Update(Measure obj)
        {
            var d = await Db.Measures.FirstOrDefaultAsync(x=>x.Id==obj.Data.Id);

            d.Code = obj.Data.Code; 
            d.Name = obj.Data.Name;
            d.Definition = obj.Data.Definition;
            d.ValidForm = obj.Data.ValidForm;
            d.ValidTo = obj.Data.ValidTo;
            Db.Measures.Update(d);
   

            try
            {
                await Db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MeasureViewExists(MeasureView.Id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }
        }
    }
}
