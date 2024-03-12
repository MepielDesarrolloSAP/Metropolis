using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gate.Clases
{
    public class Address
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Ext { get; set; }
        public string CP { get; set; }
        public string Colony { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool Enable { get; set; }

    }
}