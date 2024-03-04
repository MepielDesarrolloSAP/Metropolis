using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gate.Clases
{
    public class routedisabled
    {
        public int Id { get; set; }
        public string U_NAME { get; set; }
        public string Condition { get; set; }
        public string ConditionsType { get; set; }
        public string DocNums { get; set; }
        public string Comments { get; set; }
        public string Phone { get; set; }
        public string ShipToCode { get; set; }
        public bool Status { get; set; }
        public bool Enable { get; set; }
        public int Id_Users { get; set; }
        public int Id_FolioRoute { get; set; }
        public int Id_clients { get; set; }
    }
}