using System;

namespace Abc.Data.Common
{
    public abstract class PeriodData
    {
        public DateTime? ValidForm { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}