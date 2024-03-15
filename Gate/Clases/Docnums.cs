using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gate.Clases
{
    public class Docnums
    {
        public int Id { get; set; }
        public string DocNum { get; set; }
        public string DocDate { get; set; }
        public string VisitStatus { get; set; }
        public string Comments { get; set; }
        public bool Enable { get; set; }
        public int Id_ClientAddress { get; set; }
        public bool SimpleRoute_Status { get; set; }
        public int Id_VisitsimpleRoute { get; set; }
        public string Code { get; set; }
        public int Id_Driver { get; set; }


    }
}