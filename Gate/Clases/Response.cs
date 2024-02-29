using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Gate.Clases
{
    public class Response
    {
        public Message message { get; set; }
        public Problems Problems { get; set; }
    }
}