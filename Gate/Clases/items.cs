using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gate.Clases
{
    public class items
    {
        public int Id { get; set; }
        public int IdPallet { get; set; }
        public string ItemCode { get; set; }
        public string Sku { get; set; }
        public decimal Weight { get; set; }
        public decimal Quantity { get; set; }
        public string ItemName { get; set; }
    }
}