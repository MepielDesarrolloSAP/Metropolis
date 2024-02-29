using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gate.Clases
{
    public class Details
    {
        public int Id { get; set; }
        public int idPackage { get; set; }
        public selectPackage selectPackage { get; set; }
        public decimal quantityItems { get; set; }
        public decimal Weight { get; set; }
        public List<items> items { get; set; }
    }
}