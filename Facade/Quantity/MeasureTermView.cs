using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abc.Data.Quantity;
using Abc.Facade.Common;

namespace Abc.Facade.Quantity
{
    public sealed class MeasureTermView :CommonTermView
    {
        [Required]
        [DisplayName("Measure")]
        public string MasterId { get; set; }
    }
}
