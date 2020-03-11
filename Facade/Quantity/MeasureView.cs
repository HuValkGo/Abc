using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Abc.Facade.Quantity
{
    public class MeasureView
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string  Code{ get; set; }
        public string Definition{ get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Valid From")]
        public DateTime? ValidFrom { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Valid To")]
        public DateTime? ValidTo{ get; set; }
    }
}