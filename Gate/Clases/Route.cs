using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gate.Clases
{
    public class Route
    {
        public int Id { get; set; }
        public string ShipToCode { get; set; }
        public string CardName { get; set; }
        public string Street { get; set; }
        public string Block { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string U_NAME { get; set; }
        public string Condition { get; set; }
        public string ConditionsType { get; set; }
        public string DocNums { get; set; }
        public string Comments { get; set; }
        public string Phone { get; set; }
    }
}