using System.Collections.Generic;
using System.Threading.Tasks;
using Abc.Facade.Quantity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Abc.Soft.Areas.Quantity.Pages.Measures
{
    public class IndexModel : PageModel
    {
        private readonly Abc.Soft.Data.ApplicationDbContext _context;

        public IndexModel(Abc.Soft.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<MeasureView> MeasureView { get;set; }

        public async Task OnGetAsync()
        {
            MeasureView = await _context.Measures.ToListAsync();
        }
    }
}
