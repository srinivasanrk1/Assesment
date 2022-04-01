using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssesmentAPI.Core.Modal
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public string Status { get; set; }
        public Guid CategoryId { get; set; }
    }
}
