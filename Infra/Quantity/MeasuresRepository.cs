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
        protected internal QuantityDbContext db;
        public string SortOrder { get; set; }
        public MeasuresRepository(QuantityDbContext c)
        {
            db = c;
        }

        public async Task<List<Measure>> Get()
        {
            var list = await CreateSorted().ToListAsync();

            return list.Select(e => new Measure(e)).ToList();

        }

        private IQueryable<MeasureData> CreateSorted()
        {
            IQueryable<MeasureData> measures= from s in db.Measures select s;

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
            var d = await db.Measures.FirstOrDefaultAsync(m => m.Id == id);
            return new Measure(d);
        }

        public async Task Delete(string id)
        {
            var d = await db.Measures.FindAsync(id);

            if (d is null) return;

            db.Measures.Remove(d);
            await db.SaveChangesAsync();
        }

        public async Task Add(Measure obj)
        {
            db.Measures.Add(obj.Data);
            await db.SaveChangesAsync();
        }

        public async Task Update(Measure obj)
        {
            var d = await db.Measures.FirstOrDefaultAsync(x=>x.Id==obj.Data.Id);

            d.Code = obj.Data.Code; 
            d.Name = obj.Data.Name;
            d.Definition = obj.Data.Definition;
            d.ValidForm = obj.Data.ValidForm;
            d.ValidTo = obj.Data.ValidTo;
            db.Measures.Update(d);
   

            try
            {
                await db.SaveChangesAsync();
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
