using System.Threading.Tasks;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Abc.Pages.Quantity;
using Microsoft.AspNetCore.Mvc;

namespace Abc.Soft.Areas.Quantity.Pages.Measures
{
    public class CreateModel : MeasuresPage
    {
        public CreateModel(IMeasuresRepository r) : base(r) {}
        public IActionResult OnGet(string fixedFilter, string fixedValue)
        {
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            return Page();
        }
       
        public async Task<IActionResult> OnPostAsync(string fixedFilter, string fixedValue)
        {
            FixedFilter = fixedFilter;
            FixedValue = fixedValue;
            if (!await addObject()) return Page();
            return Redirect($"/Quantity/Measures/Index?fixedFilter={FixedFilter}&fixedValue={FixedValue}");
        }
    }
}
