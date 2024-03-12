using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gate.Clases
{
    public class Pendingpackages
    {
        public int Id { get; set; }
        public string DocNum { get; set; }
        public bool Enable { get; set; }
        public int Id_DocNums { get; set; }
    }
}