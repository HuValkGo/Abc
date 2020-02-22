using System.Collections;
using System.Collections.Generic;
using Abc.Domain.Quantity;
using Abc.Facade.Quantity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abc.Pages.Quantity
{
    public class MeasuresPage : PageModel
    {
        protected internal readonly IMeasuresRepository data;

        protected internal MeasuresPage(IMeasuresRepository r)
        {
            data = r;
        }
        [BindProperty] 
        public MeasureView Item { get; set; }
        public IList<MeasureView> Items { get; set; }
    }
}
