using System;
using System.Collections.Generic;

#nullable disable

namespace AssesmentAPI.Data.Domain
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public string Status { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
