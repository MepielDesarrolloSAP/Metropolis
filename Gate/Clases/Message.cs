using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gate.Clases
{
    public class Message
    {
        public string DocNum { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string EMail { get; set; }
        public Direction Direction { get; set; }
        public List<Details> Details { get; set; }
    }
}