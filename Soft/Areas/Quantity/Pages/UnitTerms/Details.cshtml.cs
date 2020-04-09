using System.Threading.Tasks;
using Abc.Domain.Quantity;
using Abc.Pages.Quantity;
using Microsoft.AspNetCore.Mvc;

namespace Abc.Soft.Areas.Quantity.Pages.UnitTerms
{
    public class DetailsModel : UnitTermsPage
    {
        public DetailsModel(IUnitTermsRepository r) : base(r) { }
        public async Task<IActionResult> OnGetAsync(string id, string fixedFilter, string fixedValue)
        {

            await getObject(id,fixedValue,fixedValue);
            return Page();
        }
    }
}

