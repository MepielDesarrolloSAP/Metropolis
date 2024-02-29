using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gate.Clases
{
    public class selectPackage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Lenght { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Volumetric { get; set; }
        public decimal Weight { get; set; }
    }
}