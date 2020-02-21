using System.Threading.Tasks;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abc.Soft.Areas.Quantity.Pages.Measures
{
    public class CreateModel : PageModel
    {
        private readonly IMeasuresRepository data;

        public CreateModel(IMeasuresRepository r)
        {
            data = r;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MeasureView MeasureView { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await data.Add(MeasureViewFactory.Create(MeasureView));

            return RedirectToPage("./Index");
        }
    }
}
