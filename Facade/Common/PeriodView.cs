using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Abc.Facade.Common
{
    public abstract class PeriodView
    {
        
        [DataType(DataType.Date)]
        [DisplayName("Valid From")]
        public DateTime? ValidFrom { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Valid To")]
        public DateTime? ValidTo { get; set; }
    }
}