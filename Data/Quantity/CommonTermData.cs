using Abc.Data.Common;

namespace Abc.Data.Quantity
{
    public abstract class CommonTermData:PeriodData
    {
         public string MasterId { get; set; }
         public string TermId { get; set; }
         public string Power { get; set; }
    }
}
