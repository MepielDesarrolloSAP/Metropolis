using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sap.Data.Hana;
using Gate.Properties;

namespace Gate.Clases
{
    public class Globals
    {
        //Variable de conexion a hana studio
        public static HanaConnection Con = new HanaConnection(Settings.Default.HanaConec);
        public static HanaCommand cmd = null;
        public static HanaDataReader reader = null;
    }
}