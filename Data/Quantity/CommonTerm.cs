using System;
using System.Collections.Generic;
using System.Text;
using Abc.Data.Common;

namespace Abc.Data.Quantity
{
    public class CommonTerm:PeriodData
    {
         public string MasterId { get; set; }
         public string TermId { get; set; }
         public string Power { get; set; }
    }
}
