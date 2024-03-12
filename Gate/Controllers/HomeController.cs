using Gate.Clases;
using Gate.Components.DL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Gate.Properties;
using Sap.Data.Hana;
using Microsoft.AspNet.Identity;
using System.Net.Sockets;
using System.Reflection.Emit;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Gate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                Users User = System.Web.HttpContext.Current.Session["Usuario"] as Users;

                if (User != null)
                {
                    return View();
                }
                else
                {
                    return View("~/Views/Auth/Signin.cshtml");
                }

            }
            catch (Exception x)
            {
                return View("~/Views/Auth/Signin.cshtml");
            }
        }

        public JsonResult Users()
        {
            Users user = new Users();
            List<Users> users = new List<Users>();
            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT \r\nT0.Id, \r\nT0.NameU, \r\nT0.Lastname, \r\nT0.Username, \r\nT0.Password, \r\nT0.Email, \r\nT0.Phone, \r\nT0.Enable, \r\nT0.Id_Role,\r\nT0.Id_Address,\r\nT1.Name\r\n\r\nFROM \r\nusers T0\r\ninner join Address T1 on T0.Id_Address = T1.Id;"; // where users.Enable = 0";

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        user.Id = Convert.ToInt32(row["Id"]);
                        user.Name = Convert.ToString(row["NameU"]);
                        user.Lastname = Convert.ToString(row["Lastname"]);
                        user.Username = Convert.ToString(row["Username"]);
                        user.Password = Convert.ToString(row["Password"]);
                        user.Email = Convert.ToString(row["Email"]);
                        user.Phone = Convert.ToString(row["Phone"]);
                        user.Enable = Convert.ToBoolean(row["Enable"]);
                        user.Id_Role = Convert.ToInt32(row["Id_Role"]);
                        user.Id_Address = Convert.ToInt32(row["Id_Address"]);
                        user.NameAddress = Convert.ToString(row["Name"]);
                        users.Add(user);
                        user = new Users();
                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
            return Json(users);
        }

        public JsonResult AddUser(string Name, string Lastname, string Username, string Password, string Email, string Phone, int Id_Role, string ChooseAddres)
        {
            Users NewUser = new Users();

            NewUser.Id = DL.LastIdUser() + 1;
            NewUser.Name = Name;
            NewUser.Lastname = Lastname;
            NewUser.Username = Username;
            NewUser.Password = Password;
            NewUser.Email = Email;
            NewUser.Phone = Phone;
            NewUser.Enable = true;
            NewUser.Id_Role = Id_Role;
            NewUser.Id_Address = DL.GetIdAddress(ChooseAddres);

            Problems val = DL.UserExist(NewUser.Username, NewUser.Email);

            if (!val.problem)
            {
                using (MySqlConnection conexion = DL.OpenConnectionMysql())
                {
                    try
                    {

                        string Query = "insert into Users(Id, NameU, Lastname, Username, Password, Email, Phone, Enable, Id_Role, Id_Address)\r\nvalue('" + NewUser.Id + "', '" + NewUser.Name + "', '" + NewUser.Lastname + "', '" + NewUser.Username + "', '" + NewUser.Password + "', '" + NewUser.Email + "', '" + NewUser.Phone + "', " + NewUser.Enable + ", '" + NewUser.Id_Role + "', '" + NewUser.Id_Address + "' )";

                        MySqlCommand mySqlData = new MySqlCommand(Query, conexion);
                        //MySqlDataReader reader = mySqlData.ExecuteReader();

                        int rowsAffected = mySqlData.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {

                        }
                        else
                        {
                            NewUser = new Users();
                        }

                    }
                    catch (Exception x)
                    {
                    }
                    conexion.Close();
                }

            }
            else
            {
                //string jsonString = JsonConvert.SerializeObject(val);
                //return Json(jsonString);
                return Json(val);
            }


            return Json(NewUser);
        }

        public JsonResult EditUser(int IdEdit, string NameEdit, string LastnameEdit, string UsernameEdit, string PasswordEdit, string EmailEdit, string PhoneEdit, bool EnableEdit, int Id_RoleEdit, string ChooseAddresEdit)
        {

            bool validation = false;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {

                    int Id = IdEdit;
                    string name = NameEdit;
                    string Lastname = LastnameEdit;
                    string Username = UsernameEdit;
                    string Password = PasswordEdit;
                    string Email = EmailEdit;
                    string Phone = PhoneEdit;
                    bool Enable = EnableEdit;
                    int Id_Role = Id_RoleEdit;
                    int Id_Address = DL.GetIdAddress(ChooseAddresEdit);

                    string Query = @"
                                        UPDATE users
                                        SET NameU = @Name,
                                        Lastname = @Lastname,
                                        Username = @Username,
                                        Password = @Password,
                                        Email = @Email,
                                        Phone = @Phone,
                                        Enable = @Enable,
                                        Id_Role = @Id_Role,
                                        Id_Address = @Id_Address
                                        WHERE Id = @Id";


                    using (MySqlCommand command = new MySqlCommand(Query, conexion))
                    {
                        // Asignar valores a los parámetros
                        command.Parameters.AddWithValue("@Id", Id);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Lastname", Lastname);
                        command.Parameters.AddWithValue("@Username", Username);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Phone", Phone);
                        command.Parameters.AddWithValue("@Enable", Enable);
                        command.Parameters.AddWithValue("@Id_Role", Id_Role);
                        command.Parameters.AddWithValue("@Id_Address", Id_Address);

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        // Comprobar si la actualización fue exitosa
                        if (rowsAffected > 0)
                        {
                            validation = true;
                        }
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return Json(validation);

        }

        public JsonResult DeleteUser(int Id)
        {
            bool validation = false;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {

                    string Query = @"
                                        delete from users
                                        WHERE Id = @Id";


                    using (MySqlCommand command = new MySqlCommand(Query, conexion))
                    {
                        // Asignar valores a los parámetros
                        command.Parameters.AddWithValue("@Id", Id);

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        // Comprobar si la actualización fue exitosa
                        if (rowsAffected > 0)
                        {
                            validation = true;
                        }
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return Json(validation);

        }

        public JsonResult DisableUser(int Id)
        {
            bool validation = false;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    bool Enable = false;

                    string Query = @"
                                        UPDATE users
                                        SET Enable = @Enable
                                        WHERE Id = @Id";


                    using (MySqlCommand command = new MySqlCommand(Query, conexion))
                    {
                        // Asignar valores a los parámetros
                        command.Parameters.AddWithValue("@Id", Id);
                        command.Parameters.AddWithValue("@Enable", Enable);

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        // Comprobar si la actualización fue exitosa
                        if (rowsAffected > 0)
                        {
                            validation = true;
                        }
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return Json(validation);

        }

        public JsonResult Address()
        {
            Address Address = new Address();
            List<Address> Addresses = new List<Address>();
            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    string Query = "Select * from Address"; // where users.Enable = 0";

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        Address.Id = Convert.ToInt32(row["Id"]);
                        Address.Name = Convert.ToString(row["Name"]);
                        Address.Street = Convert.ToString(row["Street"]);
                        Address.Ext = Convert.ToString(row["Ext"]);
                        Address.CP = Convert.ToString(row["CP"]);
                        Address.Colony = Convert.ToString(row["Colony"]);
                        Address.City = Convert.ToString(row["City"]);
                        Address.State = Convert.ToString(row["State"]);
                        Address.Enable = Convert.ToBoolean(row["Enable"]);
                        Addresses.Add(Address);
                        Address = new Address();
                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
            return Json(Addresses.ToList());
        }

        public JsonResult AddAddress(string NameAddress, string Street, string Ext, string CP, string Colony, string City, string State)
        {
            bool validation = false;

            int Id = DL.LastIdAddress() + 1;

            bool Enable = true;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {

                    string Query = "insert into Address(Id, Name, Street, Ext, CP, Colony, City, State, Enable)\r\nvalue('" + Id + "', '" + NameAddress + "', '" + Street + "', '" + Ext + "', '" + CP + "', '" + Colony + "', '" + City + "', '" + State + "', " + Enable + " )";

                    MySqlCommand mySqlData = new MySqlCommand(Query, conexion);
                    //MySqlDataReader reader = mySqlData.ExecuteReader();

                    int rowsAffected = mySqlData.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        validation = true;
                    }
                    else
                    {

                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return Json(validation);
        }

        public JsonResult EditAddress(int IDAddresEdit, string NameAddressEdit, string StreetEdit, string ExtEdit, string CPEdit, string ColonyEdit, string CityEdit, string StateEdit, bool EnableEdit)
        {

            bool validation = false;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {

                    string Query = @"
                                        UPDATE address
                                        SET Name = @Name,
                                        Street = @Street,
                                        Ext = @Ext,
                                        CP = @CP,
                                        Colony = @Colony,
                                        City = @City,
                                        State = @State,
                                        Enable = @Enable
                                        WHERE Id = @Id";


                    using (MySqlCommand command = new MySqlCommand(Query, conexion))
                    {
                        // Asignar valores a los parámetros
                        command.Parameters.AddWithValue("@Id", IDAddresEdit);
                        command.Parameters.AddWithValue("@Name", NameAddressEdit);
                        command.Parameters.AddWithValue("@Street", StreetEdit);
                        command.Parameters.AddWithValue("@Ext", ExtEdit);
                        command.Parameters.AddWithValue("@CP", CPEdit);
                        command.Parameters.AddWithValue("@Colony", ColonyEdit);
                        command.Parameters.AddWithValue("@City", CityEdit);
                        command.Parameters.AddWithValue("@State", StateEdit);
                        command.Parameters.AddWithValue("@Enable", EnableEdit);

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        // Comprobar si la actualización fue exitosa
                        if (rowsAffected > 0)
                        {
                            validation = true;
                        }
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return Json(validation);

        }

        public JsonResult DisableAddress(int Id)
        {
            bool validation = false;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    bool Enable = false;

                    string Query = @"
                                        UPDATE address
                                        SET Enable = @Enable
                                        WHERE Id = @Id";


                    using (MySqlCommand command = new MySqlCommand(Query, conexion))
                    {
                        // Asignar valores a los parámetros
                        command.Parameters.AddWithValue("@Id", Id);
                        command.Parameters.AddWithValue("@Enable", Enable);

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        // Comprobar si la actualización fue exitosa
                        if (rowsAffected > 0)
                        {
                            validation = true;
                        }
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return Json(validation);

        }

        //Ultimo cambio en telefono
        public JsonResult RouteMCDMX()
        {
            Route route = new  Route();
            List<Route> lista = new List<Route>();
            try
            {
                DateTime date= DateTime.Now;
                string fechaFormateada = date.ToString("yyyyMMdd");
                int Id = 0;

                //Borrar esta linea
                fechaFormateada = "2024-03-08";

                //Conexion a SAP
                Globals.Con = new HanaConnection(Settings.Default.HanaConec);
                Globals.Con.Open();

                string StrSql = "SELECT distinct\r\nT0.\"ShipToCode\" as \"CODIGO DE DESTINO\",\r\nT0.\"CardCode\" as \"CODIGO SAP\",\r\nT0.\"CardName\" as \"NOMBRE DEL CLIENTE\",\r\nT2.\"Street\" as \"DIRECCION\",\r\nT2.\"Block\" as \"COLONIA\",\r\nT2.\"ZipCode\" as \"C.P.\",\r\nT2.\"City\" as \"CIUDAD\",\r\nT3.\"U_NAME\" as \"EJECUTIVA(O)\",\r\n\r\nCASE \r\nWHEN T0.\"GroupNum\" = '2'\r\nTHEN 'CONTADO'\r\nElSE 'CREDITO'\r\nend as \"CONDICION DE PAGO\",\r\n  CASE \r\nWHEN T0.\"PeyMethod\" = '01'\r\nTHEN 'Efectivo'\r\nWHEN T0.\"PeyMethod\" = '02'\r\nTHEN 'Cheque'\r\nWHEN T0.\"PeyMethod\" = '03'\r\nTHEN 'Transferencia electronica de fondos'\r\nWHEN T0.\"PeyMethod\" = '04'\r\nTHEN 'Tarjeta de credito'\r\nWHEN T0.\"PeyMethod\" = '15'\r\nTHEN 'Condonacion'\r\nWHEN T0.\"PeyMethod\" = '17'\r\nTHEN 'Compensacion'\r\nWHEN T0.\"PeyMethod\" = '28'\r\nTHEN 'Tarjeta de debito'\r\nWHEN T0.\"PeyMethod\" = '31'\r\nTHEN 'Intermediarios bancarios'\r\nWHEN T0.\"PeyMethod\" = '99'\r\nTHEN 'Por definir'\r\nElSE 'N/A'\r\nend as \"CONDICION DE PAGO\",  \r\n (\r\nSELECT  \r\nSTRING_AGG( T12.\"DocNum\", ', ')\r\n\r\nFROM\r\n " + Properties.Settings.Default.Base +".ORDR T12\r\n\r\nWHERE\r\nT12.\"U_Ruta\" = 'R1' \r\nand  T12.\"DocDate\" = '"+ fechaFormateada + "'  \r\nand T12.\"ShipToCode\" = T0.\"ShipToCode\"\r\nand T12.\"U_Sucursal\" = '02'\r\n\r\n) as \"CANTIDAD DE OV\",\r\n ifnull(T1.\"Phone1\",'') as \"Telefono registrado en SAP\"  \r\nFROM\r\n " + Settings.Default.Base +".ORDR T0\r\nINNER JOIN "+ Settings.Default.Base +".OCRD\tT1 ON T0.\"CardCode\" = T1.\"CardCode\"\r\nINNER JOIN "+ Settings.Default.Base +".CRD1 T2 ON T1.\"CardCode\" = T2.\"CardCode\"\r\nINNER JOIN "+ Settings.Default.Base + ".OUSR T3 ON T0.\"UserSign\" = T3.\"USERID\"\r\n\r\nWHERE\r\nT0.\"U_Ruta\" = 'R1' \r\nand  T0.\"DocDate\" = '" + fechaFormateada + "'  \r\nand T0.\"ShipToCode\" = T2.\"Address\"\r\nand T0.\"U_Sucursal\" = '02'";
                Globals.cmd = new HanaCommand(StrSql, Globals.Con);
                Globals.reader = Globals.cmd.ExecuteReader();

                if (Globals.reader.HasRows)
                {
                    while (Globals.reader.Read())
                    {

                        route = new Route();
                        Id = Id+ 1;

                        route.Id = Id;
                        route.ShipToCode = Globals.reader.GetString(0);
                        route.CardCode= Globals.reader.GetString(1);
                        route.CardName = Globals.reader.GetString(2);
                        route.Street = Globals.reader.GetString(3);
                        route.Block = Globals.reader.GetString(4);
                        route.ZipCode = Globals.reader.GetString(5);
                        route.City = Globals.reader.GetString(6);
                        route.U_NAME = Globals.reader.GetString(7);
                        route.Condition = Globals.reader.GetString(8);
                        route.ConditionsType= Globals.reader.GetString(9);
                        route.DocNums = Globals.reader.GetString(10);
                        route.Phone= Globals.reader.GetString(11);

                        lista.Add(route);

                    }
                }
                Globals.reader.Close();
                Globals.Con.Close();
            }
            catch (Exception t)
            {
                string caca = t.ToString();
            }
            return Json(lista.ToList());
        }

        //Ultimo cambio en telefono
        public JsonResult RouteTCDMX()
        {
            Route route = new Route();
            List<Route> lista = new List<Route>();
            try
            {
                DateTime date = DateTime.Now;
                string fechaFormateada = date.ToString("yyyyMMdd");
                int Id = 0;

                //Borrar esta linea
                fechaFormateada = "2024-03-08";

                //Conexion a SAP
                Globals.Con = new HanaConnection(Settings.Default.HanaConec);
                Globals.Con.Open();

                string StrSql = "SELECT distinct\r\nT0.\"ShipToCode\" as \"CODIGO DE DESTINO\",\r\nT0.\"CardCode\" as \"CODIGO SAP\",\r\nT0.\"CardName\" as \"NOMBRE DEL CLIENTE\",\r\nT2.\"Street\" as \"DIRECCION\",\r\nT2.\"Block\" as \"COLONIA\",\r\nT2.\"ZipCode\" as \"C.P.\",\r\nT2.\"City\" as \"CIUDAD\",\r\nT3.\"U_NAME\" as \"EJECUTIVA(O)\",\r\n\r\nCASE \r\nWHEN T0.\"GroupNum\" = '2'\r\nTHEN 'CONTADO'\r\nElSE 'CREDITO'\r\nend as \"CONDICION DE PAGO\",\r\n  CASE \r\nWHEN T0.\"PeyMethod\" = '01'\r\nTHEN 'Efectivo'\r\nWHEN T0.\"PeyMethod\" = '02'\r\nTHEN 'Cheque'\r\nWHEN T0.\"PeyMethod\" = '03'\r\nTHEN 'Transferencia electronica de fondos'\r\nWHEN T0.\"PeyMethod\" = '04'\r\nTHEN 'Tarjeta de credito'\r\nWHEN T0.\"PeyMethod\" = '15'\r\nTHEN 'Condonacion'\r\nWHEN T0.\"PeyMethod\" = '17'\r\nTHEN 'Compensacion'\r\nWHEN T0.\"PeyMethod\" = '28'\r\nTHEN 'Tarjeta de debito'\r\nWHEN T0.\"PeyMethod\" = '31'\r\nTHEN 'Intermediarios bancarios'\r\nWHEN T0.\"PeyMethod\" = '99'\r\nTHEN 'Por definir'\r\nElSE 'N/A'\r\nend as \"CONDICION DE PAGO\",  \r\n (\r\nSELECT  \r\nSTRING_AGG( T12.\"DocNum\", ', ')\r\n\r\nFROM\r\n " + Properties.Settings.Default.Base + ".ORDR T12\r\n\r\nWHERE\r\nT12.\"U_Ruta\" = 'R2' \r\nand  T12.\"DocDate\" = '" + fechaFormateada + "'  \r\nand T12.\"ShipToCode\" = T0.\"ShipToCode\"\r\nand T12.\"U_Sucursal\" = '02'\r\n\r\n) as \"CANTIDAD DE OV\",\r\n ifnull(T1.\"Phone1\",'') as \"Telefono registrado en SAP\"   \r\nFROM\r\n " + Settings.Default.Base + ".ORDR T0\r\nINNER JOIN " + Settings.Default.Base + ".OCRD\tT1 ON T0.\"CardCode\" = T1.\"CardCode\"\r\nINNER JOIN " + Settings.Default.Base + ".CRD1 T2 ON T1.\"CardCode\" = T2.\"CardCode\"\r\nINNER JOIN " + Settings.Default.Base + ".OUSR T3 ON T0.\"UserSign\" = T3.\"USERID\"\r\n\r\nWHERE\r\nT0.\"U_Ruta\" = 'R2' \r\nand  T0.\"DocDate\" = '" + fechaFormateada + "'  \r\nand T0.\"ShipToCode\" = T2.\"Address\"\r\nand T0.\"U_Sucursal\" = '02'";
                Globals.cmd = new HanaCommand(StrSql, Globals.Con);
                Globals.reader = Globals.cmd.ExecuteReader();

                if (Globals.reader.HasRows)
                {
                    while (Globals.reader.Read())
                    {

                        route = new Route();
                        Id = Id + 1;

                        route.Id = Id;
                        route.ShipToCode = Globals.reader.GetString(0);
                        route.CardCode = Globals.reader.GetString(1);
                        route.CardName = Globals.reader.GetString(2);
                        route.Street = Globals.reader.GetString(3);
                        route.Block = Globals.reader.GetString(4);
                        route.ZipCode = Globals.reader.GetString(5);
                        route.City = Globals.reader.GetString(6);
                        route.U_NAME = Globals.reader.GetString(7);
                        route.Condition = Globals.reader.GetString(8);
                        route.ConditionsType = Globals.reader.GetString(9);
                        route.DocNums = Globals.reader.GetString(10);
                        route.Phone = Globals.reader.GetString(11);


                        lista.Add(route);

                    }
                }
                Globals.reader.Close();
                Globals.Con.Close();
            }
            catch (Exception t)
            {
                string caca = t.ToString();
            }
            return Json(lista.ToList());
        }

        //Ultimo cambio en telefono
        public JsonResult RouteMGDL()
        {
            Route route = new Route();
            List<Route> lista = new List<Route>();
            try
            {
                DateTime date = DateTime.Now;
                string fechaFormateada = date.ToString("yyyyMMdd");
                int Id = 0;

                //Borrar esta linea
                fechaFormateada = "2024-03-08";

                //Conexion a SAP
                Globals.Con = new HanaConnection(Settings.Default.HanaConec);
                Globals.Con.Open();

                string StrSql = "SELECT distinct\r\nT0.\"ShipToCode\" as \"CODIGO DE DESTINO\",\r\nT0.\"CardCode\" as \"CODIGO SAP\",\r\nT0.\"CardName\" as \"NOMBRE DEL CLIENTE\",\r\nT2.\"Street\" as \"DIRECCION\",\r\nT2.\"Block\" as \"COLONIA\",\r\nT2.\"ZipCode\" as \"C.P.\",\r\nT2.\"City\" as \"CIUDAD\",\r\nT3.\"U_NAME\" as \"EJECUTIVA(O)\",\r\n\r\nCASE \r\nWHEN T0.\"GroupNum\" = '2'\r\nTHEN 'CONTADO'\r\nElSE 'CREDITO'\r\nend as \"CONDICION DE PAGO\",\r\n  CASE \r\nWHEN T0.\"PeyMethod\" = '01'\r\nTHEN 'Efectivo'\r\nWHEN T0.\"PeyMethod\" = '02'\r\nTHEN 'Cheque'\r\nWHEN T0.\"PeyMethod\" = '03'\r\nTHEN 'Transferencia electronica de fondos'\r\nWHEN T0.\"PeyMethod\" = '04'\r\nTHEN 'Tarjeta de credito'\r\nWHEN T0.\"PeyMethod\" = '15'\r\nTHEN 'Condonacion'\r\nWHEN T0.\"PeyMethod\" = '17'\r\nTHEN 'Compensacion'\r\nWHEN T0.\"PeyMethod\" = '28'\r\nTHEN 'Tarjeta de debito'\r\nWHEN T0.\"PeyMethod\" = '31'\r\nTHEN 'Intermediarios bancarios'\r\nWHEN T0.\"PeyMethod\" = '99'\r\nTHEN 'Por definir'\r\nElSE 'N/A'\r\nend as \"CONDICION DE PAGO\", \r\n (\r\nSELECT  \r\nSTRING_AGG( T12.\"DocNum\", ', ')\r\n\r\nFROM\r\n " + Properties.Settings.Default.Base + ".ORDR T12\r\n\r\nWHERE\r\nT12.\"U_Ruta\" = 'R1' \r\nand  T12.\"DocDate\" = '" + fechaFormateada + "'  \r\nand T12.\"ShipToCode\" = T0.\"ShipToCode\"\r\nand T12.\"U_Sucursal\" = '01'\r\n\r\n) as \"CANTIDAD DE OV\", \r\n  ifnull(T1.\"Phone1\",'') as \"Telefono registrado en SAP\"    \r\nFROM\r\n " + Settings.Default.Base + ".ORDR T0\r\nINNER JOIN " + Settings.Default.Base + ".OCRD\tT1 ON T0.\"CardCode\" = T1.\"CardCode\"\r\nINNER JOIN " + Settings.Default.Base + ".CRD1 T2 ON T1.\"CardCode\" = T2.\"CardCode\"\r\nINNER JOIN " + Settings.Default.Base + ".OUSR T3 ON T0.\"UserSign\" = T3.\"USERID\"\r\n\r\nWHERE\r\nT0.\"U_Ruta\" = 'R1' \r\nand  T0.\"DocDate\" = '" + fechaFormateada + "'  \r\nand T0.\"ShipToCode\" = T2.\"Address\"\r\nand T0.\"U_Sucursal\" = '01'";
                Globals.cmd = new HanaCommand(StrSql, Globals.Con);
                Globals.reader = Globals.cmd.ExecuteReader();

                if (Globals.reader.HasRows)
                {
                    while (Globals.reader.Read())
                    {

                        route = new Route();
                        Id = Id + 1;

                        route.Id = Id;
                        route.ShipToCode = Globals.reader.GetString(0);
                        route.CardCode = Globals.reader.GetString(1);
                        route.CardName = Globals.reader.GetString(2);
                        route.Street = Globals.reader.GetString(3);
                        route.Block = Globals.reader.GetString(4);
                        route.ZipCode = Globals.reader.GetString(5);
                        route.City = Globals.reader.GetString(6);
                        route.U_NAME = Globals.reader.GetString(7);
                        route.Condition = Globals.reader.GetString(8);
                        route.ConditionsType = Globals.reader.GetString(9);
                        route.DocNums = Globals.reader.GetString(10);
                        route.Phone = Globals.reader.GetString(11);


                        lista.Add(route);

                    }
                }
                Globals.reader.Close();
                Globals.Con.Close();
            }
            catch (Exception t)
            {
                string caca = t.ToString();
            }
            return Json(lista.ToList());
        }

        //Ultimo cambio en telefono
        public JsonResult RouteTGDL()
        {
            Route route = new Route();
            List<Route> lista = new List<Route>();
            try
            {
                DateTime date = DateTime.Now;
                string fechaFormateada = date.ToString("yyyyMMdd");
                int Id = 0;

                //Borrar esta linea
                fechaFormateada = "2024-03-08";

                //Conexion a SAP
                Globals.Con = new HanaConnection(Settings.Default.HanaConec);
                Globals.Con.Open();

                string StrSql = "SELECT distinct\r\nT0.\"ShipToCode\" as \"CODIGO DE DESTINO\",\r\nT0.\"CardCode\" as \"CODIGO SAP\",\r\nT0.\"CardName\" as \"NOMBRE DEL CLIENTE\",\r\nT2.\"Street\" as \"DIRECCION\",\r\nT2.\"Block\" as \"COLONIA\",\r\nT2.\"ZipCode\" as \"C.P.\",\r\nT2.\"City\" as \"CIUDAD\",\r\nT3.\"U_NAME\" as \"EJECUTIVA(O)\",\r\n\r\nCASE \r\nWHEN T0.\"GroupNum\" = '2'\r\nTHEN 'CONTADO'\r\nElSE 'CREDITO'\r\nend as \"CONDICION DE PAGO\",\r\n  CASE \r\nWHEN T0.\"PeyMethod\" = '01'\r\nTHEN 'Efectivo'\r\nWHEN T0.\"PeyMethod\" = '02'\r\nTHEN 'Cheque'\r\nWHEN T0.\"PeyMethod\" = '03'\r\nTHEN 'Transferencia electronica de fondos'\r\nWHEN T0.\"PeyMethod\" = '04'\r\nTHEN 'Tarjeta de credito'\r\nWHEN T0.\"PeyMethod\" = '15'\r\nTHEN 'Condonacion'\r\nWHEN T0.\"PeyMethod\" = '17'\r\nTHEN 'Compensacion'\r\nWHEN T0.\"PeyMethod\" = '28'\r\nTHEN 'Tarjeta de debito'\r\nWHEN T0.\"PeyMethod\" = '31'\r\nTHEN 'Intermediarios bancarios'\r\nWHEN T0.\"PeyMethod\" = '99'\r\nTHEN 'Por definir'\r\nElSE 'N/A'\r\nend as \"CONDICION DE PAGO\", \r\n (\r\nSELECT  \r\nSTRING_AGG( T12.\"DocNum\", ', ')\r\n\r\nFROM\r\n " + Properties.Settings.Default.Base + ".ORDR T12\r\n\r\nWHERE\r\nT12.\"U_Ruta\" = 'R2' \r\nand  T12.\"DocDate\" = '" + fechaFormateada + "'  \r\nand T12.\"ShipToCode\" = T0.\"ShipToCode\"\r\nand T12.\"U_Sucursal\" = '01'\r\n\r\n) as \"CANTIDAD DE OV\",\r\n  ifnull(T1.\"Phone1\",'') as \"Telefono registrado en SAP\"   \r\nFROM\r\n " + Settings.Default.Base + ".ORDR T0\r\nINNER JOIN " + Settings.Default.Base + ".OCRD\tT1 ON T0.\"CardCode\" = T1.\"CardCode\"\r\nINNER JOIN " + Settings.Default.Base + ".CRD1 T2 ON T1.\"CardCode\" = T2.\"CardCode\"\r\nINNER JOIN " + Settings.Default.Base + ".OUSR T3 ON T0.\"UserSign\" = T3.\"USERID\"\r\n\r\nWHERE\r\nT0.\"U_Ruta\" = 'R2' \r\nand  T0.\"DocDate\" = '" + fechaFormateada + "'  \r\nand T0.\"ShipToCode\" = T2.\"Address\"\r\nand T0.\"U_Sucursal\" = '01'";
                Globals.cmd = new HanaCommand(StrSql, Globals.Con);
                Globals.reader = Globals.cmd.ExecuteReader();

                if (Globals.reader.HasRows)
                {
                    while (Globals.reader.Read())
                    {

                        route = new Route();
                        Id = Id + 1;

                        route.Id = Id;
                        route.ShipToCode = Globals.reader.GetString(0);
                        route.CardCode = Globals.reader.GetString(1);
                        route.CardName = Globals.reader.GetString(2);
                        route.Street = Globals.reader.GetString(3);
                        route.Block = Globals.reader.GetString(4);
                        route.ZipCode = Globals.reader.GetString(5);
                        route.City = Globals.reader.GetString(6);
                        route.U_NAME = Globals.reader.GetString(7);
                        route.Condition = Globals.reader.GetString(8);
                        route.ConditionsType = Globals.reader.GetString(9);
                        route.DocNums = Globals.reader.GetString(10);

                        lista.Add(route);

                    }
                }
                Globals.reader.Close();
                Globals.Con.Close();
            }
            catch (Exception t)
            {
                string caca = t.ToString();
            }
            return Json(lista.ToList());
        }

        public JsonResult GeneratedRoutesMCDMX()
        {
            InfoRoute route = new InfoRoute();
            List<InfoRoute> lista = new List<InfoRoute>();

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT \r\nT0.Id, \r\nT0.CreateDate,\r\nT1.Name,\r\nT1.Description\r\n\r\nFROM \r\nfolioroute T0 \r\ninner join typeofroute T1 on T0.Id_typeofroute = T1.Id\r\n\r\nwhere\r\nT1.Id = '3' and T0.Enable = 1; ";

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        route.Id = Convert.ToInt32(row["Id"]);
                        route.CreateDate = Convert.ToString(row["CreateDate"]);
                        route.Name = Convert.ToString(row["Name"]);
                        route.Description = Convert.ToString(row["Description"]);

                        lista.Add(route);
                        route = new InfoRoute();

                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return Json(lista.ToList());
        }

        public JsonResult GeneratedRoutesTCDMX()
        {
            InfoRoute route = new InfoRoute();
            List<InfoRoute> lista = new List<InfoRoute>();

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT \r\nT0.Id, \r\nT0.CreateDate,\r\nT1.Name,\r\nT1.Description\r\n\r\nFROM \r\nfolioroute T0 \r\ninner join typeofroute T1 on T0.Id_typeofroute = T1.Id\r\n\r\nwhere\r\nT1.Id = '4' and T0.Enable = 1;";

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        route.Id = Convert.ToInt32(row["Id"]);
                        route.CreateDate = Convert.ToString(row["CreateDate"]);
                        route.Name = Convert.ToString(row["Name"]);
                        route.Description = Convert.ToString(row["Description"]);

                        lista.Add(route);
                        route = new InfoRoute();

                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return Json(lista.ToList());
        }

        public JsonResult GeneratedRoutesMGDL()
        {
            InfoRoute route = new InfoRoute();
            List<InfoRoute> lista = new List<InfoRoute>();

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT \r\nT0.Id, \r\nT0.CreateDate,\r\nT1.Name,\r\nT1.Description\r\n\r\nFROM \r\nfolioroute T0 \r\ninner join typeofroute T1 on T0.Id_typeofroute = T1.Id\r\n\r\nwhere\r\nT1.Id = '1' and T0.Enable = 1;";

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        route.Id = Convert.ToInt32(row["Id"]);
                        route.CreateDate = Convert.ToString(row["CreateDate"]);
                        route.Name = Convert.ToString(row["Name"]);
                        route.Description = Convert.ToString(row["Description"]);

                        lista.Add(route);
                        route = new InfoRoute();

                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return Json(lista.ToList());
        }

        public JsonResult GeneratedRoutesTGDL()
        {
            InfoRoute route = new InfoRoute();
            List<InfoRoute> lista = new List<InfoRoute>();

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT \r\nT0.Id, \r\nT0.CreateDate,\r\nT1.Name,\r\nT1.Description\r\n\r\nFROM \r\nfolioroute T0 \r\ninner join typeofroute T1 on T0.Id_typeofroute = T1.Id\r\n\r\nwhere\r\nT1.Id = '2' and T0.Enable = 1;";

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        route.Id = Convert.ToInt32(row["Id"]);
                        route.CreateDate = Convert.ToString(row["CreateDate"]);
                        route.Name = Convert.ToString(row["Name"]);
                        route.Description = Convert.ToString(row["Description"]);

                        lista.Add(route);
                        route = new InfoRoute();

                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return Json(lista.ToList());
        }

        //Traer Json de ruta guardada por folio de ruta
        public JsonResult FindMCDMX(int Id)
        {
            Route route = new Route();
            List<Route> lista = new List<Route>();

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT\r\n  T0.Id\r\n, T3.ShipToCode\r\n, T2.CardName\r\n, T3.Street\r\n, T3.Colony\r\n, T3.ZipCode\r\n, T3.City\r\n, T0.U_NAME\r\n, T0.Conditions\r\n, T0.ConditionsType\r\n, T0.docnums\r\n, T0.Comments\r\n, T0.Phone\r\n\r\nFROM \r\nroute T0\r\ninner join folioroute T1 on T0.Id_FolioRoute = T1.Folio\r\ninner join clients T2 on T0.Id_clients = T2.Id\r\ninner join clientaddress T3 on T2.Id = T3.Id_clients and T0.ShipToCode = T3.ShipToCode\r\n\r\nwhere T1.Id = "+Id+" \r\n\r\norder by T0.Id asc";

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        route.Id = Convert.ToInt32(row["Id"]);
                        route.ShipToCode = Convert.ToString(row["ShipToCode"]);
                        route.CardName = Convert.ToString(row["CardName"]);
                        route.Street = Convert.ToString(row["Street"]);
                        route.Block = Convert.ToString(row["Colony"]);
                        route.ZipCode = Convert.ToString(row["ZipCode"]);
                        route.City = Convert.ToString(row["City"]);
                        route.U_NAME = Convert.ToString(row["U_NAME"]);
                        route.Condition = Convert.ToString(row["Conditions"]);
                        route.ConditionsType = Convert.ToString(row["ConditionsType"]);
                        route.DocNums = Convert.ToString(row["DocNums"]);
                        route.Comments = Convert.ToString(row["Comments"]);
                        route.Phone = Convert.ToString(row["Phone"]);


                        lista.Add(route);
                        route = new Route();

                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return Json(lista.ToList());
        }

        public JsonResult OVRouteMCDMX(int OvText)
        {
            OV route = new OV();
            try
            {

                int Id = 0;

                //Conexion a SAP
                Globals.Con = new HanaConnection(Settings.Default.HanaConec);
                Globals.Con.Open();

                //Con filtro de almacen
                //string StrSql = "SELECT distinct\r\nT0.\"ShipToCode\" as \"CODIGO DE DESTINO\",\r\nT0.\"CardName\" as \"NOMBRE DEL CLIENTE\",\r\nT2.\"Street\" as \"DIRECCION\",\r\nT2.\"Block\" as \"COLONIA\",\r\nT2.\"ZipCode\" as \"C.P.\",\r\nT2.\"City\" as \"CIUDAD\",\r\nT3.\"U_NAME\" as \"EJECUTIVA(O)\",\r\n\r\nCASE \r\nWHEN T0.\"GroupNum\" = '2'\r\nTHEN 'CONTADO'\r\nElSE 'CREDITO'\r\nend as \"CONDICION DE PAGO\",\r\n\r\n T0.\"DocNum\", \r\n\r\n T0.\"U_Ruta\",  \r\n\r\n T0.\"DocDate\"  \r\n\r\nFROM\r\n " + Settings.Default.Base + ".ORDR T0\r\nINNER JOIN " + Settings.Default.Base + ".OCRD\tT1 ON T0.\"CardCode\" = T1.\"CardCode\"\r\nINNER JOIN " + Settings.Default.Base + ".CRD1 T2 ON T1.\"CardCode\" = T2.\"CardCode\"\r\nINNER JOIN " + Settings.Default.Base + ".OUSR T3 ON T0.\"UserSign\" = T3.\"USERID\"\r\n\r\nWHERE\r\nT0.\"ShipToCode\" = T2.\"Address\"\r\n and T0.\"U_Sucursal\" = '02' and T0.\"DocNum\" = '" + OvText + "'";

                //Sing filtro
                string StrSql = "SELECT distinct\r\nT0.\"ShipToCode\" as \"CODIGO DE DESTINO\",\r\nT0.\"CardName\" as \"NOMBRE DEL CLIENTE\",\r\nT2.\"Street\" as \"DIRECCION\",\r\nT2.\"Block\" as \"COLONIA\",\r\nT2.\"ZipCode\" as \"C.P.\",\r\nT2.\"City\" as \"CIUDAD\",\r\nT3.\"U_NAME\" as \"EJECUTIVA(O)\",\r\n\r\nCASE \r\nWHEN T0.\"GroupNum\" = '2'\r\nTHEN 'CONTADO'\r\nElSE 'CREDITO'\r\nend as \"CONDICION DE PAGO\", CASE \r\nWHEN T0.\"PeyMethod\" = '01'\r\nTHEN 'Efectivo'\r\nWHEN T0.\"PeyMethod\" = '02'\r\nTHEN 'Cheque'\r\nWHEN T0.\"PeyMethod\" = '03'\r\nTHEN 'Transferencia electronica de fondos'\r\nWHEN T0.\"PeyMethod\" = '04'\r\nTHEN 'Tarjeta de credito'\r\nWHEN T0.\"PeyMethod\" = '15'\r\nTHEN 'Condonacion'\r\nWHEN T0.\"PeyMethod\" = '17'\r\nTHEN 'Compensacion'\r\nWHEN T0.\"PeyMethod\" = '28'\r\nTHEN 'Tarjeta de debito'\r\nWHEN T0.\"PeyMethod\" = '31'\r\nTHEN 'Intermediarios bancarios'\r\nWHEN T0.\"PeyMethod\" = '99'\r\nTHEN 'Por definir'\r\nElSE 'N/A'\r\nend as \"CONDICION DE PAGO\",   \r\n\r\n T0.\"DocNum\", \r\n\r\n T0.\"U_Ruta\",  \r\n\r\n T0.\"DocDate\"  \r\n\r\nFROM\r\n " + Settings.Default.Base + ".ORDR T0\r\nINNER JOIN " + Settings.Default.Base + ".OCRD\tT1 ON T0.\"CardCode\" = T1.\"CardCode\"\r\nINNER JOIN " + Settings.Default.Base + ".CRD1 T2 ON T1.\"CardCode\" = T2.\"CardCode\"\r\nINNER JOIN " + Settings.Default.Base + ".OUSR T3 ON T0.\"UserSign\" = T3.\"USERID\"\r\n\r\nWHERE\r\nT0.\"ShipToCode\" = T2.\"Address\"\r\n  and T0.\"DocNum\" = '"+ OvText +"'";
                
                Globals.cmd = new HanaCommand(StrSql, Globals.Con);
                Globals.reader = Globals.cmd.ExecuteReader();

                if (Globals.reader.HasRows)
                {
                    while (Globals.reader.Read())
                    {

                        route = new OV();
                        Id = Id + 1;

                        route.Id = Id;
                        route.ShipToCode = Globals.reader.GetString(0);
                        route.CardName = Globals.reader.GetString(1);
                        route.Street = Globals.reader.GetString(2);
                        route.Block = Globals.reader.GetString(3);
                        route.ZipCode = Globals.reader.GetString(4);
                        route.City = Globals.reader.GetString(5);
                        route.U_NAME = Globals.reader.GetString(6);
                        route.Condition = Globals.reader.GetString(7);
                        route.ConditionsType = Globals.reader.GetString(8);
                        route.DocNums = Globals.reader.GetString(9);
                        route.Route = Globals.reader.GetString(10);
                        route.DocDate = Globals.reader.GetString(11);
                        break;
                    }
                }
                Globals.reader.Close();
                Globals.Con.Close();
            }
            catch (Exception t)
            {
                string caca = t.ToString();
            }
            return Json(route);
        }

        #region examples
        //EXAMPLE HANA DATA
        public JsonResult BuscarFactura(long Factura)
        {
            //DatosFactura Datos = new DatosFactura();
            //List<DatosFactura> lista = new List<DatosFactura>();
            //try
            //{
            //    string Base = "";

            //    if (Properties.Settings.Default.Productivo == "SI")
            //    {
            //        Base = Properties.Settings.Default.BaseSap;
            //    }
            //    else
            //    {
            //        Base = Properties.Settings.Default.BaseSapPruebas;
            //    }

            //    //Conexion a SAP
            //    Globals.Con = new HanaConnection(Settings.Default.HanaConec);
            //    Globals.Con.Open();

            //    string StrSql = "SELECT  DISTINCT ifnull(T2.\"DocNum\",0), T3.\"StreetS\", T3.\"BlockS\",T3.\"CityS\",T3.\"ZipCodeS\",T3.\"StateS\", T3.\"CountryS\", T0.\"CardCode\", T0.\"CardName\",  T0.\"DocEntry\" \r\nFROM\r\n" + Base + ".OINV T0\r\nINNER JOIN " + Base + ".INV1 T1 ON T0.\"DocEntry\" = T1.\"DocEntry\"\r\nLEFT JOIN " + Base + ".ORDR T2 ON T1.\"BaseEntry\" = T2.\"DocEntry\"\r\nINNER JOIN " + Base + ".INV12 T3 ON T0.\"DocEntry\" = T3.\"DocEntry\"\r\nINNER JOIN " + Base + ".OCRD T4 ON T0.\"CardCode\" = T4.\"CardCode\" \r\n\r\n WHERE T0.\"DocNum\" = '" + Factura + "'";
            //    Globals.cmd = new HanaCommand(StrSql, Globals.Con);
            //    Globals.reader = Globals.cmd.ExecuteReader();

            //    if (Globals.reader.HasRows)
            //    {
            //        while (Globals.reader.Read())
            //        {
            //            Datos.DocNum = Globals.reader.GetString(0);
            //            Datos.Calle = Globals.reader.GetString(1);
            //            Datos.Colonia = Globals.reader.GetString(2);
            //            Datos.Ciudad = Globals.reader.GetString(3);
            //            Datos.Cp = Globals.reader.GetString(4);
            //            Datos.Estado = Globals.reader.GetString(5);
            //            Datos.Pais = Globals.reader.GetString(6);
            //            Datos.CardCode = Globals.reader.GetString(7);
            //            Datos.CardName = Globals.reader.GetString(8);
            //            Datos.DocEntry = Convert.ToInt32(Globals.reader.GetString(9));
            //            break;
            //        }
            //    }
            //    Globals.reader.Close();

            //    StrSql = "SELECT  ifnull(T1.\"E_MailL\",'-'), ifnull(T1.\"Tel1\",'-'), T0.\"DocEntry\" \r\nFROM\r\n" + Base + ".OCRD T0  Inner join " + Base + ".OCPR T1 ON T0.\"CardCode\" = T1.\"CardCode\"  \r\n\r\nWHERE T0.\"CardCode\" = '" + Datos.CardCode + "'";
            //    Globals.cmd = new HanaCommand(StrSql, Globals.Con);
            //    Globals.reader = Globals.cmd.ExecuteReader();

            //    if (Globals.reader.HasRows)
            //    {
            //        while (Globals.reader.Read())
            //        {
            //            Datos.Email = Globals.reader.GetString(0);
            //            Datos.Tel1 = Globals.reader.GetString(1);
            //            lista.Add(Datos);
            //            Datos = new DatosFactura();
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        Datos.Email = "";
            //        Datos.Tel1 = "-";
            //        lista.Add(Datos);
            //        Datos = new DatosFactura();
            //    }
            //    Globals.reader.Close();
            //    Globals.Con.Close();
            //}
            //catch (Exception t)
            //{
            //    string caca = t.ToString();
            //}
            //return Json(lista.ToList());
            return Json("");
        }

        //Ejemplo Post
        public async Task<JsonResult> BuscarPedido(long Pedido)
        {
            string respuesta;
            try
            {
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri("http://172.16.101.191:8080/dealermepiel/guide?orderid=" + Pedido);
                    client.BaseAddress = new Uri("http://172.16.101.128:8080/dealermepiel/guide?orderid=" + Pedido);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "c2yc10cH1J3068kKLSCK6f1Ts75qe8PpUmVa69P6.oKLLWBq16Dmj8n/myf6");
                    var response = client.GetAsync("").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        respuesta = await response.Content.ReadAsStringAsync();
                        if (respuesta.Contains("<br />"))
                        {
                            respuesta = "Error";
                        }
                    }
                    else
                    {
                        respuesta = "Error";
                    }
                }
            }
            catch (Exception t)
            {
                respuesta = "Error";
            }

            return Json(respuesta.ToString());

        }
        #endregion

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult SaveRoute(List<Route> Route, string laruta)
        {
            bool val = false;

            int Id_typeofroute = 0;

            int AddressExist = 0;

            if (laruta == "R1")
            {
                Id_typeofroute = 1;
            }
            else if (laruta == "R2")
            {
                Id_typeofroute = 2;
            }
            else if (laruta == "R3")
            {
                Id_typeofroute = 3;
            }
            else if (laruta == "R4")
            {
                Id_typeofroute = 4;
            }

            int idclient = 0;

            //bool valRoute = false;

            Users User = System.Web.HttpContext.Current.Session["Usuario"] as Users;

            if (User != null)
            {

                using (MySqlConnection conexion = DL.OpenConnectionMysql())
                {
                    try
                    {
                        int folio = DL.AddFolioRoute(Id_typeofroute);

                        if (folio > 0)
                        {
                            foreach (var v in Route)
                            {

                                idclient = DL.ClientExist(v.CardCode);

                                //Existe cliente//
                                if (idclient != 0)
                                {

                                    //falta validar si ya se guardo anteriormente la ruta.
                                    val = DL.DocNumsExist(v.DocNums);
                                    if (val == true)
                                    {
                                        //nada
                                    }
                                    else
                                    {
                                        //Guardar ruta
                                        val = DL.AddRoute(v.U_NAME, v.Condition, v.ConditionsType, v.DocNums, v.Comments, v.Phone, v.ShipToCode, true, User.Id, folio, idclient);
                                        if (val == false)
                                        {
                                            return Json(val);
                                        }

                                        AddressExist = DL.ClientAddressExist(idclient, v.ShipToCode);

                                        if (AddressExist != 0)
                                        {
                                            val = val = DL.AddDocNums(v.DocNums, AddressExist, folio);
                                            if (val == false)
                                            {
                                                return Json(val);
                                            }
                                        }
                                        else
                                        {
                                            //Guardar direccion de cliente
                                            val = DL.AddClientAddress(v.ShipToCode, v.Street, v.Block, v.ZipCode, v.City, idclient, v.DocNums, folio);
                                            if (val == false)
                                            {
                                                return Json(val);
                                            }
                                        }
                                    }
                                }
                                //No existe cliente//
                                else
                                {
                                    //Guardamos cliente
                                    idclient = DL.AddClient(v.CardName, v.CardCode, v.Phone);

                                    //Guardar ruta
                                    val = DL.AddRoute(v.U_NAME, v.Condition, v.ConditionsType, v.DocNums, v.Comments, v.Phone,v.ShipToCode ,true, User.Id, folio, idclient);
                                    if (val == false)
                                    {
                                        return Json(val);
                                    }

                                    //Guardar direccion de cliente
                                    val = DL.AddClientAddress(v.ShipToCode, v.Street, v.Block, v.ZipCode, v.City, idclient, v.DocNums,folio);
                                    if (val == false)
                                    {
                                        return Json(val);
                                    }

                                }

                            }
                        }
                        else
                        {
                            return Json("RutaG");
                        }

                    }
                    catch (Exception x)
                    {
                    }
                    conexion.Close();
                }

            }
            else
            {

                return Json("login");

            }

            return Json(val);
        }

        public  JsonResult routedisabled(List<Ids> ListIds)
        {
            bool val = false;
            foreach (var va in ListIds) 
            {
                //cambiar valor de campo enable de true a false en tabla route donde coincide Id
                val = DL.DisableRoute(va.Id);

                if (val == false)
                {
                    return Json(val);
                }

                val = DL.DisableDocNums(va.DocNums);

                if (val == false)
                {
                    return Json(val);
                }

            }

            return Json(val);
        }


    }
}