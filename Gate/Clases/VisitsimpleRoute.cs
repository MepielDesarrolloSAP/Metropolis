using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gate.Clases
{
    public class VisitsimpleRoute
    {
        public int Id { get; set; }
        public decimal Id_SimpleRoutevisit { get; set; } 
        public string Id_SimpleRouteRoute { get; set; }
        public int Id_drivers { get; set; }
        public decimal Id_DriverSimpleRoute { get; set; }
        public string Pictures { get; set; }
        public string Date { get; set; }
        public string ETime_arrival { get; set; }
        public string ReferenceDocNums { get; set; }


    }
}