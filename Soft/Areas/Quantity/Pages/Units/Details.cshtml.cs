using System.Threading.Tasks;
using Abc.Domain.Quantity;
using Abc.Pages.Quantity;
using Microsoft.AspNetCore.Mvc;

namespace Abc.Soft.Areas.Quantity.Pages.Units
{
    public class DetailsModel : UnitsPage
    {
        public DetailsModel(IUnitsRepository r) : base(r) { }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            await getObject(id);
            return Page();
        }
    }
}

