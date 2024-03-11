using Gate.Clases;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Gate.Properties;
using System.Web.Routing;
using Microsoft.Ajax.Utilities;
using System.Drawing;
using System.Reflection.Emit;
using Newtonsoft.Json;
using Sap.Data.Hana;
using Newtonsoft.Json.Linq;
using Mysqlx.Prepare;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Gate.Components.DL
{
    public class DL
    {
        public static HanaConnection Con = new HanaConnection(Settings.Default.HanaConec);

        public static HanaCommand cmdOne = null;
        public static HanaDataReader readerOne = null;

        public static HanaCommand cmdTwo = null;
        public static HanaDataReader readerTwo = null;

        public static HanaCommand cmdthree = null;
        public static HanaDataReader readerthree = null;

        public static MySqlConnection OpenConnectionMysql()
        {
            MySqlConnection connection = new MySqlConnection();
            string server = string.Empty;
            server = Settings.Default.Baseexterna;
            string DB = string.Empty;
            DB = "metropolis";
            string User = string.Empty;
            User = Settings.Default.Usuario;
            string Password = string.Empty;
            Password = Settings.Default.contraseña;
            string Port = string.Empty;
            Port = "3306";

            string CadenaConexion = "server=" + server + ";" + "port=" + Port + ";" + "user id=" + User + ";" + "password=" + Password + ";" + "database=" + DB + ";";

            try
            {
                connection.ConnectionString = CadenaConexion;
                connection.Open();
            }
            catch (MySqlException ex)
            {
                string error = ex.Message;
            }

            return connection;
        }

        public static Users FindUser(string UsernamePar, string PasswordPar)
        {
            Users user = new Users();
            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    string Query = "Select * from users T0 where (T0.Username ='" + UsernamePar + "' or T0.Email = '" + UsernamePar + "') and T0.Password = '" + PasswordPar + "'"; // where users.Enable = 0";

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
                        break;
                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
            return user;
        }

        public static Problems UserExist(string UsernamePar, string CorreoPar)
        {
            Problems pro = new Problems();
            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT * FROM metropolis.users T0  where T0.Username = '"+ UsernamePar + "' or Email = '" + CorreoPar + "';"; // val user exist

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        pro.problem = true;
                        pro.description = " Usuario o Email ya registrados";
                        break;
                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
            return pro;
        }

        public static Users FindUserTwo(string UsernamePar)
        {
            Users user = new Users();
            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    string Query = "Select * from users T0 where (T0.Username ='" + UsernamePar + "' or T0.Email = '" + UsernamePar + "')"; // where users.Enable = 0";

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
                        break;
                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
            return user;
        }

        public static void DisableUser(string UserName)
        {

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {

                    string Query = " UPDATE users SET Enable = 0 WHERE (Username = '"+ UserName + "' or Email = '"+ UserName + "')";


                    using (MySqlCommand command = new MySqlCommand(Query, conexion))
                    {

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        // Comprobar si la actualización fue exitosa
                        if (rowsAffected > 0)
                        {

                        }
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
        }

        public static void EnableUser(string UserName)
        {

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {

                    string Query = " UPDATE users SET Enable = 1 WHERE (Username = '" + UserName + "' or Email = '" + UserName + "')";


                    using (MySqlCommand command = new MySqlCommand(Query, conexion))
                    {

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        // Comprobar si la actualización fue exitosa
                        if (rowsAffected > 0)
                        {

                        }
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
        }

        //Obtener ultimo Id agregado en usuarios
        public static int LastIdUser()
        {
            int lastId = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    // Obtiene el último ID de la tabla 'caca'
                    string Query = "SELECT MAX(id) as LastID FROM Users";
                    MySqlCommand lastIdCommand = new MySqlCommand(Query, conexion);
                    lastId = Convert.ToInt32(lastIdCommand.ExecuteScalar());

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
            return lastId;
        }

        //Obtener ultimo Id agregado en users
        public static int LastIdAddress()
        {
            int lastId = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    // Obtiene el último ID de la tabla 'caca'
                    string Query = "SELECT MAX(id) as LastID FROM Address";
                    MySqlCommand lastIdCommand = new MySqlCommand(Query, conexion);
                    lastId = Convert.ToInt32(lastIdCommand.ExecuteScalar());

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
            return lastId;
        }

        //Obtener ultimo Id agregado en FolioRoute
        public static int LastIdAFolioRoute()
        {
            int lastId = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    // Obtiene el último ID de la tabla 'caca'
                    string Query = "SELECT MAX(id) as LastID FROM FolioRoute";
                    MySqlCommand lastIdCommand = new MySqlCommand(Query, conexion);
                    //if(lastIdCommand.LastInsertedId == 0)
                    //{
                    //lastId = 1;
                    //}
                    //else
                    //{
                    lastId = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                    //}

                }
                catch (Exception x)
                {
                    lastId = 0;
                }
                conexion.Close();
            }
            return lastId;
        }

        //Obtener ultimo Id agregado en users
        public static int LastIdRoute()
        {
            int lastId = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    // Obtiene el último ID de la tabla 'caca'
                    string Query = "SELECT MAX(id) as LastID FROM Route";
                    MySqlCommand lastIdCommand = new MySqlCommand(Query, conexion);
                    //if (lastIdCommand.LastInsertedId == 0)
                    //{
                    //    lastId = 1;
                    //}
                    //else
                    //{
                    lastId = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                    //}

                }
                catch (Exception x)
                {
                    lastId = 0;
                }
                conexion.Close();
            }
            return lastId;
        }

        //Obtener ultimo Id agregado en clients
        public static int LastIdClient()
        {
            int lastId = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    // Obtiene el último ID de la tabla 'caca'
                    string Query = "SELECT MAX(id) as LastID FROM Clients";
                    MySqlCommand lastIdCommand = new MySqlCommand(Query, conexion);
                    //if (lastIdCommand.LastInsertedId == 0)
                    //{
                    //    lastId = 1;
                    //}
                    //else
                    //{
                    lastId = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                    //}

                }
                catch (Exception x)
                {
                    lastId = 0;
                }
                conexion.Close();
            }
            return lastId;
        }

        //Obtener ultimo Id agregado en direcciones de clients
        public static int LastIdClientAddress()
        {
            int lastId = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    // Obtiene el último ID de la tabla 'caca'
                    string Query = "SELECT MAX(id) as LastID FROM ClientAddress";
                    MySqlCommand lastIdCommand = new MySqlCommand(Query, conexion);
                    //if (lastIdCommand.LastInsertedId == 0)
                    //{
                    //    lastId = 1;
                    //}
                    //else
                    //{
                    lastId = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                    //}

                }
                catch (Exception x)
                {
                    lastId = 0;
                }
                conexion.Close();
            }
            return lastId;
        }

        //Obtener ultimo Id agregado en docnums
        public static int LastIdDocNums()
        {
            int lastId = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    // Obtiene el último ID de la tabla 'caca'
                    string Query = "SELECT MAX(id) as LastID FROM Docnums";
                    MySqlCommand lastIdCommand = new MySqlCommand(Query, conexion);
                    //if (lastIdCommand.LastInsertedId == 0)
                    //{
                    //    lastId = 1;
                    //}
                    //else
                    //{
                    lastId = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                    //}

                }
                catch (Exception x)
                {
                    lastId = 0;
                }
                conexion.Close();
            }
            return lastId;
        }

        //Obtener ultimo Id agregado en Packages
        public static int LastIdPackages()
        {
            int lastId = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    // Obtiene el último ID de la tabla 'caca'
                    string Query = "SELECT MAX(id) as LastID FROM Packages";
                    MySqlCommand lastIdCommand = new MySqlCommand(Query, conexion);
                    //if (lastIdCommand.LastInsertedId == 0)
                    //{
                    //    lastId = 1;
                    //}
                    //else
                    //{
                    lastId = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                    //}

                }
                catch (Exception x)
                {
                    lastId = 0;
                }
                conexion.Close();
            }
            return lastId;
        }

        //Obtener ultimo Id agregado en Packages
        public static int LastIdPendingPackages()
        {
            int lastId = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    // Obtiene el último ID de la tabla 'caca'
                    string Query = "SELECT MAX(id) as LastID FROM PendingPackages";
                    MySqlCommand lastIdCommand = new MySqlCommand(Query, conexion);
                    //if (lastIdCommand.LastInsertedId == 0)
                    //{
                    //    lastId = 1;
                    //}
                    //else
                    //{
                    lastId = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                    //}

                }
                catch (Exception x)
                {
                    lastId = 0;
                }
                conexion.Close();
            }
            return lastId;
        }

        //Obtener ultimo Id agregado en Packages
        public static int LastIdPackagedetails()
        {
            int lastId = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    // Obtiene el último ID de la tabla 'caca'
                    string Query = "SELECT MAX(id) as LastID FROM Packagedetails";
                    MySqlCommand lastIdCommand = new MySqlCommand(Query, conexion);
                    //if (lastIdCommand.LastInsertedId == 0)
                    //{
                    //    lastId = 1;
                    //}
                    //else
                    //{
                    lastId = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                    //}

                }
                catch (Exception x)
                {
                    lastId = 0;
                }
                conexion.Close();
            }
            return lastId;
        }

        //Obtener Direccion Id en base a nombre de direccion
        public static int GetIdAddress(string Name)
        {
            int Id = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    // Obtiene el último ID de la tabla 'caca'
                    string Query = "SELECT T0.Id  FROM address T0 where T0.Name = '" + Name+"'";
                    MySqlCommand lastIdCommand = new MySqlCommand(Query, conexion);
                    Id = Convert.ToInt32(lastIdCommand.ExecuteScalar());

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
            return Id;
        }

        public static int AddFolioRoute(int Id_typeofroute, List<Gate.Clases.Route> Route)
        {

            int folio = 0;

            bool val = false;

            int idclient = 0;

            foreach (var v in Route)
            {

                idclient = DL.ClientExist(v.CardCode);

                //Existe cliente//
                if (idclient != 0)
                {

                    //falta validar si ya se guardo anteriormente la ruta.
                    val = DL.DocNumsExist(v.DocNums);

                }

                break;
            }

            if (val == true)
            {
                return folio;
            }

            int FolioRoute = LastIdAFolioRoute() + 1;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    DateTime date = DateTime.Now;
                    string fechaFormateada = date.ToString("yyyy-MM-dd");

                    string Query = "insert into FolioRoute(Id, Folio,CreateDate,Enable,Id_typeofroute)\r\nvalue('" + FolioRoute + "', '" + FolioRoute + "','" + fechaFormateada + "','" + true + "','" + Id_typeofroute + "');";

                    MySqlCommand mySqlData = new MySqlCommand(Query, conexion);
                    //MySqlDataReader reader = mySqlData.ExecuteReader();

                    int rowsAffected = mySqlData.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        folio = FolioRoute;
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

            return folio;
        }

        public static int ClientExist(string CardCode)
        {
            int idClient = 0;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT T0.Id FROM Clients T0 where  T0.CardCode = '"+ CardCode +"'"; // where users.Enable = 0";

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        idClient = Convert.ToInt32(row["Id"]);
                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return idClient;

        }

        public static int ClientAddressExist(int idClient, string ClientAddress)
        {
            int AddressExist = 0;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT T0.Id FROM clientaddress T0 where  T0.Id_clients = '" + idClient + "' and T0.ShipToCode = '" + ClientAddress + "'"; // where users.Enable = 0";

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        AddressExist = Convert.ToInt32(row["Id"]);
                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return AddressExist;

        }

        public static int AddClient(string CardName, string CardCode, string phone)
        {
            int folio = 0;

            int FolioClient = LastIdClient() + 1;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {

                    string Query = "insert into Clients(Id, CardName,CardCode,Phone)\r\nvalue('" + FolioClient + "', '" + CardName + "','" + CardCode + "','"+ phone +"');";

                    MySqlCommand mySqlData = new MySqlCommand(Query, conexion);
                    //MySqlDataReader reader = mySqlData.ExecuteReader();

                    int rowsAffected = mySqlData.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        folio = FolioClient;
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

            return folio;
        }

        public static bool AddRoute(string U_NAME, string Conditions, string ConditionsType, string DocNums, string Comments, string Phone, string ShipToCode, bool enable, int Id_Users, int Id_FolioRoute, int Id_clients)
        {
            bool val = false;

            int idroute = DL.LastIdRoute() + 1;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {

                    string Query = "insert into Route(Id, U_NAME,Conditions,ConditionsType,DocNums,Comments,Phone,ShipToCode,Enable,Id_Users,Id_FolioRoute,Id_Clients)\r\nvalue('" + idroute + "', '" + U_NAME + "', '" + Conditions + "', '" + ConditionsType + "','" + DocNums + "','" + Comments + "','" + Phone + "', '" + ShipToCode + "', '" + enable + "', '" + Id_Users + "', '" + Id_FolioRoute + "', '" + Id_clients + "')";

                    MySqlCommand mySqlData = new MySqlCommand(Query, conexion);
                    //MySqlDataReader reader = mySqlData.ExecuteReader();

                    int rowsAffected = mySqlData.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        val = true;
                    }
                    else
                    {
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
                return val;
            }

        }

        public static bool AddClientAddress(string ShipToCode, string Street, string Colony, string ZipCode, string City, int Id_clients,string docnums)
        {
            bool val = false;

            int IdClientAddress = DL.LastIdClientAddress() + 1;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {

                    string Query = "insert into ClientAddress(Id, ShipToCode,Street,Colony,ZipCode,City,Id_Clients,Reference)\r\nvalue('" + IdClientAddress + "', '" + ShipToCode + "', '" + Street + "', '" + Colony + "','" + ZipCode + "','" + City + "','" + Id_clients + "','')";

                    MySqlCommand mySqlData = new MySqlCommand(Query, conexion);
                    //MySqlDataReader reader = mySqlData.ExecuteReader();

                    int rowsAffected = mySqlData.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        val = AddDocNums(docnums, IdClientAddress);
                    }
                    else
                    {
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
                return val;
            }

        }

        public static bool DocNumsExist(string docnums)
        {
            bool val = false;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {

                    string[] registros = docnums.Split(new string[] { ", " }, StringSplitOptions.None);

                    foreach (string Order in registros)
                    {
                        string Query = "SELECT T0.DocNum FROM docnums T0 where  T0.DocNum = '" + Order + "' and T0.Enable = '1'";

                        MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                        DataTable data = new DataTable();
                        mySqlData.Fill(data);

                        foreach (DataRow row in data.Rows)
                        {
                            val = true;
                        }
                        if (val)
                        {
                            break;
                        }
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
                return val;
            }

        }

        public static bool AddDocNums(string docnums, int IdClientAddress)
        {
            bool val = false;

            int IdDocNums = 0;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    DateTime date = DateTime.Now;
                    string fechaFormateada = date.ToString("yyyy-MM-dd");

                    //string a = "72193, 72194, 72196";
                    string[] registros = docnums.Split(new string[] { ", " }, StringSplitOptions.None);

                    if(registros.Length > 1)
                    {

                        string code = "";

                        bool v = true;

                        while (v)
                        {
                            code = generateCode();
                            v = CodeInDocNumExist(code);
                        }

                        foreach (string Order in registros)
                        {
                            IdDocNums = DL.LastIdDocNums() + 1;

                            

                            string Query = "insert into Docnums(Id,DocNum,DocDate,visitStatus,comments,Enable,Id_ClientAddress,SimpleRoute_Status,Id_VisitsimpleRoute,Code,Id_Driver)\r\nvalue('" + IdDocNums + "', '" + Order + "', '" + fechaFormateada + "','','','" + true + "','" + IdClientAddress + "','"+ false + "','','"+ code +"','')";

                            MySqlCommand mySqlData = new MySqlCommand(Query, conexion);
                            //MySqlDataReader reader = mySqlData.ExecuteReader();

                            int rowsAffected = mySqlData.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                val = AddPackages(Order, IdDocNums);
                            }

                        }
                    }

                    else
                    {
                        foreach (string Order in registros)
                        {
                            IdDocNums = DL.LastIdDocNums() + 1;
                            string Query = "insert into Docnums(Id, DocNum,DocDate,visitStatus,comments,Enable,Id_ClientAddress,SimpleRoute_Status, Id_VisitsimpleRoute, Code,Id_Driver)\r\nvalue('" + IdDocNums + "', '" + Order + "', '" + fechaFormateada + "','','','" + true + "','" + IdClientAddress + "','" + false + "','','','')";

                            MySqlCommand mySqlData = new MySqlCommand(Query, conexion);
                            //MySqlDataReader reader = mySqlData.ExecuteReader();

                            int rowsAffected = mySqlData.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                val = AddPackages(Order, IdDocNums);
                            }

                        }
                    }


                }
                catch (Exception x)
                {
                }
                conexion.Close();
                return val;
            }

        }

        public static bool CodeInDocNumExist(string code)
        {
            bool val = false;
            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT * FROM Docnums T0  where T0.Code = '" + code + "'"; // val user exist

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        val = true;
                        break;
                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
            return val;
        }

        public static string generateCode()
        {
            const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < 4; i++)
            {
                int index = rnd.Next(caracteres.Length);
                sb.Append(caracteres[index]);
            }

            return sb.ToString();
        }

        public static bool AddPackages(string Order, int IdDocNums)
        {
            bool val = false;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    dynamic jsonObject = PackingList(Order);

                    jsonObject = JsonConvert.DeserializeObject<JObject>(jsonObject);

                    //si paking esta vacio  agregar a PendingPackages y regresar true.
                    string Docnum = jsonObject["message"]["DocNum"];

                    if(Docnum == null)
                    {
                        int IdPendingPackages = LastIdPendingPackages() + 1;
                        string QueryTwo = "insert into pendingpackages(Id, Docnum,Enable,Id_Docnums)\r\nvalue('" + IdPendingPackages + "', '" + Order + "', '" + true + "','" + IdDocNums + "')";

                        MySqlCommand mySqlDataTwo = new MySqlCommand(QueryTwo, conexion);
                        //MySqlDataReader reader = mySqlData.ExecuteReader();

                        int rowsAffectedTwo = mySqlDataTwo.ExecuteNonQuery();

                        if (rowsAffectedTwo > 0)
                        {
                            val = true;
                            return val;
                        }

                    }


                    // Acceder a los valores
                    var details = jsonObject["message"]["Details"];

                    foreach (var detail in details)
                    {
                        int LastIdPackage = LastIdPackages() + 1;

                        string Query = "insert into Packages(Id, NamePackage,Lenght,Width,Height,Volumetric,Weight,quantityItems,Id_Docnums)\r\nvalue('" + LastIdPackage + "', '"+ detail["selectPackage"]["Name"] + "','"+ detail["selectPackage"]["Lenght"] + "','"+ detail["selectPackage"]["Width"] + "','"+ detail["selectPackage"]["Height"] + "','"+ detail["selectPackage"]["Volumetric"] + "','"+ detail["selectPackage"]["Weight"] + "','"+ detail["quantityItems"] + "','"+ IdDocNums + "');";

                        MySqlCommand mySqlData = new MySqlCommand(Query, conexion);
                        //MySqlDataReader reader = mySqlData.ExecuteReader();

                        int rowsAffected = mySqlData.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            var items = detail["items"];
                            foreach (var item in items)
                            {
                                int Packagedetails = LastIdPackagedetails() + 1;
                                string QueryTwo = "insert into Packagedetails(Id, ItemCode,Sku,Weight,Quantity,ItemName,Id_Packages)\r\nvalue('" + Packagedetails + "', '" + item["ItemCode"] + "', '" + item["Sku"] + "','"+ item["Weight"] + "','"+ item["Quantity"] + "','" + item["ItemName"] + "','" + LastIdPackage + "')";

                                MySqlCommand mySqlDataTwo = new MySqlCommand(QueryTwo, conexion);
                                //MySqlDataReader reader = mySqlData.ExecuteReader();

                                int rowsAffectedTwo = mySqlDataTwo.ExecuteNonQuery();

                                if (rowsAffectedTwo > 0)
                                {
                                    val = true;
                                }

                            }
                        }


                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return val;
        }

        public static string PackingList(string Order)
        {
            //1
            Response response = new Response();

            //2
            Message message = new Message();
            Problems problems = new Problems();

            //3
            Direction direction = new Direction();
            List<Details> ListDetails = new List<Details>();

            //4
            Details details = new Details();

            //5
            selectPackage selectPackage = new selectPackage();
            List<items> ListItems = new List<items>(); //YA

            //6
            items Items = new items(); //YA

            int id = 0; //YA
            int IdPallet = 0; //Ya
            int IdPalletVal = 0; //Ya

            decimal quantityItems = 0; //Ya
            decimal WeightItems = 0;

            try
            {

                //Conexion a SAP
                Con = new HanaConnection(Settings.Default.HanaConec);
                Con.Open();

                string StrSqlOne = "select Distinct\r\n        T2.\"ItemCode\",\r\n        T5.\"ItemName\",\r\n        T5.\"CodeBars\",\r\n        T5.\"BWeight1\",\r\n        T3.\"idPallet\",\r\n        T1.\"DisplayMode\",\r\n        T2.\"UomQty\",\r\n        T4.\"CardCode\",\r\n        T4.\"CardName\",\r\n        T6.\"E_Mail\",        \r\n        (T5.\"BWeight1\" * T2.\"UomQty\") AS \"Weight\",\r\n        T7.\"Street\",\r\n        T7.\"Block\" AS \"neighborhood\",\r\n        T7.\"ZipCode\", \r\n        T7.\"City\",\r\n        T7.\"State\" AS \"StateCode\",\r\n        T7.\"State\" AS \"State\",\r\n        'Mexico' AS \"country\",\r\n        T3.\"PkgCode\"\r\n        from         \r\n        PRIZMADB.WWOPM T1\r\n        inner join PRIZMADB.WWOPI T2 on T2.\"idWhsOp\" = T1.\"idWhsOp\"\r\n        inner join PRIZMADB.WPALM T3 ON T3.\"idPallet\" = T2.\"idPalletDest\"\r\n        inner join SB1CSL.ORDR T4 ON T4.\"DocNum\" = T1.\"DisplayMode\"\r\n        inner join SB1CSL.OITM T5 ON T5.\"ItemCode\"=T2.\"ItemCode\"\r\n        inner join SB1CSL.OCRD T6 ON T6.\"CardCode\"= T4.\"CardCode\"\r\n        left join SB1CSL.CRD1 T7 ON T7.\"Address\"= T4.\"ShipToCode\"\r\n        AND T7.\"CardCode\"= T4.\"CardCode\"\r\n        where T1.\"DisplayMode\"='" + Order + "'\r\n        ORDER BY T3.\"idPallet\" ";
                cmdOne = new HanaCommand(StrSqlOne, Con);
                readerOne = cmdOne.ExecuteReader();
                if (readerOne.HasRows)
                {
                    //1 While
                    while (readerOne.Read())
                    {

                        IdPallet = Convert.ToInt32(readerOne.GetString(4));

                        if (IdPalletVal != IdPallet)
                        {
                            IdPalletVal = IdPallet;
                            details = new Details();
                            details.Id = IdPallet;
                            details.idPackage = Convert.ToInt32(readerOne.GetString(18));

                            string StrSqlTwo = "SELECT  T1.\"PkgCode\" AS \"id\",\r\n                                                  T1.\"PkgType\" AS \"Name\",\r\n                                                  T1.\"Length1\" AS \"Lenght\",\r\n                                                  T1.\"Width1\"  AS \"Width\",\r\n                                                  T1.\"Height1\" AS \"Height\",\r\n                                                  T1.\"Volume\"/5000  AS \"Volumetric\",\r\n                                                 T1.\"Weight1\"  AS \"Weight\"\r\n                                                  FROM PRIZMADB.\"SOPKG\" T1 \r\n                                                  \r\n                                                  WHERE T1.\"PkgCode\" =  '" + details.idPackage + "' ";
                            cmdTwo = new HanaCommand(StrSqlTwo, Con);
                            readerTwo = cmdTwo.ExecuteReader();

                            if (readerTwo.HasRows)
                            {
                                //2 While
                                while (readerTwo.Read())
                                {
                                    selectPackage.Id = Convert.ToInt32(readerTwo.GetString(0));
                                    selectPackage.Name = readerTwo.GetString(1);
                                    selectPackage.Lenght = readerTwo.GetDecimal(2);
                                    selectPackage.Width = readerTwo.GetDecimal(3);
                                    selectPackage.Height = readerTwo.GetDecimal(4);
                                    selectPackage.Volumetric = readerTwo.GetDecimal(5);
                                    selectPackage.Weight = readerTwo.GetDecimal(6);
                                }
                                readerTwo.Close();
                            }
                            details.selectPackage = selectPackage;
                            selectPackage = new selectPackage();

                            string StrSqlthree = "select Distinct\r\n        T2.\"ItemCode\",\r\n        T5.\"ItemName\",\r\n        T5.\"CodeBars\",\r\n        T5.\"BWeight1\",\r\n        T3.\"idPallet\",\r\n        T1.\"DisplayMode\",\r\n        T2.\"UomQty\",\r\n        T4.\"CardCode\",\r\n        T4.\"CardName\",\r\n        T6.\"E_Mail\",        \r\n        (T5.\"BWeight1\" * T2.\"UomQty\") AS \"Weight\",\r\n        T7.\"Street\",\r\n        T7.\"Block\" AS \"neighborhood\",\r\n        T7.\"ZipCode\", \r\n        T7.\"City\",\r\n        T7.\"State\" AS \"StateCode\",\r\n        T7.\"State\" AS \"State\",\r\n        'Mexico' AS \"country\",\r\n        T3.\"PkgCode\"\r\n        from         \r\n        PRIZMADB.WWOPM T1\r\n        inner join PRIZMADB.WWOPI T2 on T2.\"idWhsOp\" = T1.\"idWhsOp\"\r\n        inner join PRIZMADB.WPALM T3 ON T3.\"idPallet\" = T2.\"idPalletDest\"\r\n        inner join SB1CSL.ORDR T4 ON T4.\"DocNum\" = T1.\"DisplayMode\"\r\n        inner join SB1CSL.OITM T5 ON T5.\"ItemCode\"=T2.\"ItemCode\"\r\n        inner join SB1CSL.OCRD T6 ON T6.\"CardCode\"= T4.\"CardCode\"\r\n        left join SB1CSL.CRD1 T7 ON T7.\"Address\"= T4.\"ShipToCode\"\r\n        AND T7.\"CardCode\"= T4.\"CardCode\"\r\n        where T1.\"DisplayMode\"='" + Order + "'\r\n        ORDER BY T3.\"idPallet\" ";
                            cmdthree = new HanaCommand(StrSqlOne, Con);
                            readerthree = cmdthree.ExecuteReader();
                            if (readerthree.HasRows)
                            {
                                //3 While
                                while (readerthree.Read())
                                {

                                    Items = new items();

                                    Items.IdPallet = Convert.ToInt32(readerthree.GetString(4));
                                    if (Items.IdPallet == details.Id)
                                    {
                                        id = id + 1;
                                        Items.Id = id;
                                        Items.ItemCode = readerthree.GetString(0);
                                        Items.Sku = readerthree.GetString(2);
                                        Items.Weight = readerthree.GetDecimal(3);

                                        Items.Quantity = readerthree.GetDecimal(6);

                                        Items.Weight = Items.Weight * Items.Quantity;

                                        //contador numero de items
                                        quantityItems = quantityItems + Items.Quantity;

                                        //Contador peso de todos los items
                                        WeightItems = WeightItems + Items.Weight;

                                        Items.ItemName = readerthree.GetString(1);

                                        ListItems.Add(Items);

                                    }
                                    else
                                    {
                                        id = 0;
                                    }

                                }
                                readerthree.Close();
                                details.quantityItems = quantityItems;
                                details.Weight = WeightItems;
                                details.items = ListItems;

                                quantityItems = 0;
                                WeightItems = 0;

                                ListItems = new List<items>();
                            }

                            ListDetails.Add(details);
                        }

                        else
                        {

                        }


                    }
                }
                readerOne.Close();


                #region comentado
                //StrSqlOne = "select Distinct\r\n        T2.\"ItemCode\",\r\n        T5.\"ItemName\",\r\n        T5.\"CodeBars\",\r\n        T5.\"BWeight1\",\r\n        T3.\"idPallet\",\r\n        T1.\"DisplayMode\",\r\n        T2.\"UomQty\",\r\n        T4.\"CardCode\",\r\n        T4.\"CardName\",\r\n        T6.\"E_Mail\",        \r\n        (T5.\"BWeight1\" * T2.\"UomQty\") AS \"Weight\",\r\n        T7.\"Street\",\r\n        T7.\"Block\" AS \"neighborhood\",\r\n        T7.\"ZipCode\", \r\n        T7.\"City\",\r\n        T7.\"State\" AS \"StateCode\",\r\n        T7.\"State\" AS \"State\",\r\n        'Mexico' AS \"country\",\r\n        T3.\"PkgCode\"\r\n        from         \r\n        PRIZMADB.WWOPM T1\r\n        inner join PRIZMADB.WWOPI T2 on T2.\"idWhsOp\" = T1.\"idWhsOp\"\r\n        inner join PRIZMADB.WPALM T3 ON T3.\"idPallet\" = T2.\"idPalletDest\"\r\n        inner join SB1CSL.ORDR T4 ON T4.\"DocNum\" = T1.\"DisplayMode\"\r\n        inner join SB1CSL.OITM T5 ON T5.\"ItemCode\"=T2.\"ItemCode\"\r\n        inner join SB1CSL.OCRD T6 ON T6.\"CardCode\"= T4.\"CardCode\"\r\n        left join SB1CSL.CRD1 T7 ON T7.\"Address\"= T4.\"ShipToCode\"\r\n        AND T7.\"CardCode\"= T4.\"CardCode\"\r\n        where T1.\"DisplayMode\"='" + Order + "'\r\n        ORDER BY T3.\"idPallet\" ";
                //cmdOne = new HanaCommand(StrSqlOne, Con);
                //readerOne = cmdOne.ExecuteReader();
                //if (readerOne.HasRows)
                //{
                //    //2 while
                //    while (readerOne.Read())
                //    {
                //        items1.Id = id + 1;
                //        items1.ItemCode = readerOne.GetString(0);
                //        items1.ItemName = readerOne.GetString(1);
                //        items1.Sku = readerOne.GetString(2);
                //        items1.Weight = readerOne.GetDecimal(3);
                //        items1.Quantity = Convert.ToInt32(readerOne.GetString(6).Replace(".000000", ""));

                //        items.Add(items1);
                //        items1 = new items();

                //        SumItems = SumItems + Convert.ToInt32(readerOne.GetString(6).Replace(".000000", ""));
                //        SumWeight = SumWeight + Convert.ToDecimal(readerOne.GetString(10));

                //        if (IdPack != Convert.ToInt32(readerOne.GetString(18).Replace(".000000", "")))
                //        {
                //            details1.quantityItems = SumItems;
                //            SumItems = 0;

                //            details1.Weight = SumWeight;
                //            SumWeight = 0;

                //            details1.items = items;
                //            items = new List<items>();

                //            details.Add(details1);
                //            details1 = new Details();


                //            IdPackage = IdPackage + 1;
                //            details1.Id = IdPackage;
                //            details1.idPackage = Convert.ToInt32(readerOne.GetString(18).Replace(".000000", ""));
                //            IdPack = details1.idPackage;

                //            string StrSqlTwo = "SELECT  T1.\"PkgCode\" AS \"id\",\r\n                                                  T1.\"PkgType\" AS \"Name\",\r\n                                                  T1.\"Length1\" AS \"Lenght\",\r\n                                                  T1.\"Width1\"  AS \"Width\",\r\n                                                  T1.\"Height1\" AS \"Height\",\r\n                                                  T1.\"Volume\"/5000  AS \"Volumetric\",\r\n                                                 T1.\"Weight1\"  AS \"Weight\"\r\n                                                  FROM PRIZMADB.\"SOPKG\" T1 \r\n                                                  \r\n                                                  WHERE T1.\"PkgCode\" =  '" + details1.idPackage + "' ";
                //            cmdTwo = new HanaCommand(StrSqlTwo, Con);
                //            readerTwo = cmdTwo.ExecuteReader();

                //            if (readerTwo.HasRows)
                //            {
                //                while (readerTwo.Read())
                //                {
                //                    selectPackage.Id = Convert.ToInt32(readerTwo.GetString(0).Replace(".000000", ""));
                //                    selectPackage.Name = readerTwo.GetString(1);
                //                    selectPackage.Lenght = readerTwo.GetDecimal(2);
                //                    selectPackage.Width = readerTwo.GetDecimal(3);
                //                    selectPackage.Height = readerTwo.GetDecimal(4);
                //                    selectPackage.Volumetric = readerTwo.GetDecimal(5);
                //                    selectPackage.Weight = readerTwo.GetDecimal(6);
                //                }
                //                readerTwo.Close();
                //            }
                //            details1.selectPackage = selectPackage;
                //            selectPackage = new selectPackage();

                //        }
                //    }
                //}
                //readerOne.Close();

                //details1.quantityItems = SumItems;
                //SumItems = 0;

                //details1.Weight = SumWeight;
                //SumWeight = 0;

                //details1.items = items;
                //items = new List<items>();

                //details.Add(details1);
                //details1 = new Details();
                #endregion

                StrSqlOne = "select Distinct\r\n        T2.\"ItemCode\",\r\n        T5.\"ItemName\",\r\n        T5.\"CodeBars\",\r\n        T5.\"BWeight1\",\r\n        T3.\"idPallet\",\r\n        T1.\"DisplayMode\",\r\n        T2.\"UomQty\",\r\n        T4.\"CardCode\",\r\n        T4.\"CardName\",\r\n        T6.\"E_Mail\",        \r\n        (T5.\"BWeight1\" * T2.\"UomQty\") AS \"Weight\",\r\n        T7.\"Street\",\r\n        T7.\"Block\" AS \"neighborhood\",\r\n        T7.\"ZipCode\", \r\n        T7.\"City\",\r\n        T7.\"State\" AS \"StateCode\",\r\n        T7.\"State\" AS \"State\",\r\n        'Mexico' AS \"country\",\r\n        T3.\"PkgCode\"\r\n        from         \r\n        PRIZMADB.WWOPM T1\r\n        inner join PRIZMADB.WWOPI T2 on T2.\"idWhsOp\" = T1.\"idWhsOp\"\r\n        inner join PRIZMADB.WPALM T3 ON T3.\"idPallet\" = T2.\"idPalletDest\"\r\n        inner join SB1CSL.ORDR T4 ON T4.\"DocNum\" = T1.\"DisplayMode\"\r\n        inner join SB1CSL.OITM T5 ON T5.\"ItemCode\"=T2.\"ItemCode\"\r\n        inner join SB1CSL.OCRD T6 ON T6.\"CardCode\"= T4.\"CardCode\"\r\n        left join SB1CSL.CRD1 T7 ON T7.\"Address\"= T4.\"ShipToCode\"\r\n        AND T7.\"CardCode\"= T4.\"CardCode\"\r\n        where T1.\"DisplayMode\"='" + Order + "'\r\n        ORDER BY T3.\"idPallet\" ";
                cmdOne = new HanaCommand(StrSqlOne, Con);
                readerOne = cmdOne.ExecuteReader();
                if (readerOne.HasRows)
                {
                    //4 while
                    while (readerOne.Read())
                    {
                        message.DocNum = readerOne.GetString(5);
                        message.CardCode = readerOne.GetString(7);
                        message.CardName = readerOne.GetString(8);
                        message.EMail = readerOne.GetString(9);

                        direction.Street = readerOne.GetString(11);
                        direction.neighborhood = readerOne.GetString(12);
                        direction.ZipCode = readerOne.GetString(13);
                        direction.City = readerOne.GetString(14);
                        direction.StateCode = readerOne.GetString(15);
                        direction.country = readerOne.GetString(16);

                        message.Direction = direction;
                        message.Details = ListDetails;
                        direction = new Direction();
                        break;
                    }

                }
                readerOne.Close();
                Con.Close();

                response.message = message;
                response.Problems = problems;

            }
            catch (Exception t)
            {
                string caca = t.ToString();
                problems.problem = true;
                problems.description = caca;
                response.Problems = problems;
                response.message = message;
            }

            string json = JsonConvert.SerializeObject(response);

            return json;

        }

        public static Gate.Clases.routedisabled FindRoute()
        {
            Gate.Clases.routedisabled route = new Clases.routedisabled();

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT \r\nT0.Id,\r\nT0.U_NAME,\r\nT0.Conditions,\r\nT0.ConditionsType,\r\nT0.DocNums,\r\nT0.Comments,\r\nT0.Phone,\r\nT0.enable,\r\nT0.Id_Users,\r\nT0.Id_FolioRoute,\r\nT0.Id_clients,\r\nT0.ShipToCode\r\n\r\nFROM \r\n routedisabled T0\r\n\r\n where T0.Status = '1';";

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        route.Id = Convert.ToInt32(row["Id"]);
                        route.U_NAME = Convert.ToString(row["U_NAME"]);
                        route.Condition = Convert.ToString(row["Conditions"]);
                        route.ConditionsType = Convert.ToString(row["ConditionsType"]);
                        route.DocNums = Convert.ToString(row["DocNums"]);
                        route.Comments = Convert.ToString(row["Comments"]);
                        route.Phone = Convert.ToString(row["Phone"]);
                        route.ShipToCode = Convert.ToString(row["ShipToCode"]);
                        route.Enable = Convert.ToBoolean(row["Enable"]);
                        route.Status = true;
                        route.Id_Users = Convert.ToInt32(row["Id_Users"]);
                        route.Id_FolioRoute = Convert.ToInt32(row["Id_FolioRoute"]);
                        route.Id_clients = Convert.ToInt32(row["Id_clients"]);
                    }
                }
                catch (Exception x)
                {
                }

                conexion.Close();
            }

            return route;
        }

        public static bool DisableRoute(int id)
        {
            bool val = false;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {

                    string Query = " UPDATE route SET Enable = 0 WHERE (Id = '" + id + "')";


                    using (MySqlCommand command = new MySqlCommand(Query, conexion))
                    {

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        // Comprobar si la actualización fue exitosa
                        if (rowsAffected > 0)
                        {
                            val = true;
                        }
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return val;
        }

        public static bool Changeroutedisabled(int id)
        {
            bool val = false;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {

                    string Query = " UPDATE routedisabled SET Status = 0 WHERE (Id = '" + id + "')";


                    using (MySqlCommand command = new MySqlCommand(Query, conexion))
                    {

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        // Comprobar si la actualización fue exitosa
                        if (rowsAffected > 0)
                        {
                            val = true;
                        }
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return val;
        }

        public static bool DisableDocNums(string DocNums)
        {
            bool val = false;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    string[] registros = DocNums.Split(new string[] { ", " }, StringSplitOptions.None);

                    foreach (string Order in registros)
                    {

                        string Query = " UPDATE Docnums SET Enable = 0 WHERE (DocNum = '" + Order + "')";

                        using (MySqlCommand command = new MySqlCommand(Query, conexion))
                        {

                            // Ejecutar la consulta
                            int rowsAffected = command.ExecuteNonQuery();

                            // Comprobar si la actualización fue exitosa
                            if (rowsAffected > 0)
                            {
                                val = true;
                            }
                        }
                    
                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return val;
        }

        //Revisar
        public static async Task<string> Drivers()
        {
            string respuesta;

            bool val = false;

            string Status = "";
            int Code = 0;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.simpliroute.com/v1/accounts/drivers/");
                    client.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Token 8834f564ed5abed860e13f3a6e72ab05150dc557");
                    var response = client.GetAsync("").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        respuesta = await response.Content.ReadAsStringAsync();

                        // Deserializar el JSON
                        dynamic jsonObject = JsonConvert.DeserializeObject(respuesta);

                        foreach (var details in jsonObject)
                        {
                            Status = details.status;

                            //Active
                            if (Status == "active")
                            {
                                Code = details.id;
                                //Validar si existe con consulta
                                val = DriverExist(Code);

                                //Si existe
                                if (val)
                                {
                                    //Nada
                                }
                                //Si no existe lo agrego
                                else
                                {
                                    //Agregar chofer
                                    AddDriver(details.name, Code);

                                }
                            }
                            //Deleted
                            else
                            {
                                //Validar si ya existe para desabilitarlo con una consulta
                                Code = details.id;

                                val = DriverExist(Code);

                                if (val)
                                {
                                    val = DriverStatus(Code);

                                    if (val)
                                    {
                                        //Desabilitar driver
                                        DisableDriver(Code);
                                    }
                                    else
                                    {
                                        //Nada
                                    }

                                }
                                else
                                {
                                    //Nada
                                }

                            }


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

            //return Json(respuesta.ToString());
            return respuesta;
        }

        //Revisar
        public static async Task<string> Visits()
        {
            Code C= new Code();
            C.Codigo = "";
            string code = "";

            string VarU = "";
            string respuesta;

            bool Val = false;

            DateTime date = DateTime.Now;
            date = date.AddDays(-1); // Restar un día
            string fechaFormateada = date.ToString("yyyy-MM-dd");

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.simpliroute.com/v1/routes/visits?planned_date="+ fechaFormateada);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Token 8834f564ed5abed860e13f3a6e72ab05150dc557");
                    var response = client.GetAsync("").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        respuesta = await response.Content.ReadAsStringAsync();

                        // Deserializar el JSON
                        dynamic jsonObject = JsonConvert.DeserializeObject(respuesta);

                        foreach (var details in jsonObject)
                        {
                            //Referencia
                            VarU = details.reference;

                            //Con referencia
                            if (VarU != "")
                            {
                                string[] registros = VarU.Split(new string[] { ", " }, StringSplitOptions.None);

                                foreach (string Order in registros)
                                {
                                    Docnums d = DocNumExist(Order);

                                    code = d.Code;

                                    if(code != "")
                                    {
                                        d.VisitStatus = details.status;
                                        d.Comments = details.notes;
                                        d.SimpleRoute_Status = true;
                                        d.Id_VisitsimpleRoute = details.id;
                                        d.Id_Driver = details.driver;

                                        Val = EditDocNum(d);


                                    }
                                    else
                                    {
                                        d.VisitStatus = details.status;
                                        d.Comments = details.notes;
                                        d.SimpleRoute_Status = true;
                                        d.Id_VisitsimpleRoute = details.id;
                                        d.Id_Driver = details.driver;

                                        Val = EditDocNum(d);
                                    }

                                    #region comentado
                                    //if (d.Id != 0)
                                    //{
                                    //    d.VisitStatus = details.status;
                                    //    d.Comments = details.notes;
                                    //    d.SimpleRoute_Status = true;
                                    //    d.Id_VisitsimpleRoute = details.id;
                                    //    d.Id_Driver = details.driver;

                                    //    Val = EditDocNum(d);

                                    //}
                                    //else
                                    //{
                                    //    //Nada
                                    //}
                                    #endregion

                                }



                            }
                            //Sin referencia
                            else
                            {
                                //Nada
                            }


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

            //return Json(respuesta.ToString());
            return respuesta;
        }

        public static void Addsynchronizationlog()
        {

            int Idsynchronizationlog = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    DateTime date = DateTime.Now;
                    string fecha = date.ToString("yyyy-MM-dd");
                    string fechaDia = date.ToString("dddd");

                    Idsynchronizationlog = DL.LastIdSynchronizationlog() + 1;
                    string Query = "insert into synchronizationlog(Id, CreateDate, Day)\r\nvalue('" + Idsynchronizationlog + "', '" + fecha + "', '" + fechaDia + "')";

                    MySqlCommand mySqlData = new MySqlCommand(Query, conexion);
                    //MySqlDataReader reader = mySqlData.ExecuteReader();

                    int rowsAffected = mySqlData.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

        }

        public static bool DisableDriver(int Id)
        {
            bool validation = false;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    bool Enable = false;

                    string Query = @"
                                        UPDATE drivers
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

            return (validation);

        }

        public static bool DriverExist(int idsimpleroute)
        {
            bool val = false;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT * FROM drivers T0  where T0.Id_SimpleRoute = '" + idsimpleroute + "'";

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        val = true; 
                        break;
                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
            return val;
        }

        public static bool DriverStatus(int idsimpleroute)
        {
            bool val = false;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT * FROM drivers T0  where T0.Id_SimpleRoute = '" + idsimpleroute + "'";

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        val = Convert.ToBoolean(row["Enable"]);
                        break;
                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
            return val;
        }

        //Obtener ultimo Id agregado en Drivers
        public static int LastIdDrivers()
        {
            int lastId = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    // Obtiene el último ID de la tabla 'caca'
                    string Query = "SELECT MAX(id) as LastID FROM drivers";
                    MySqlCommand lastIdCommand = new MySqlCommand(Query, conexion);
                    //if (lastIdCommand.LastInsertedId == 0)
                    //{
                    //    lastId = 1;
                    //}
                    //else
                    //{
                    lastId = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                    //}

                }
                catch (Exception x)
                {
                    lastId = 0;
                }
                conexion.Close();
            }
            return lastId;
        }

        public static int LastIdSynchronizationlog()
        {
            int lastId = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    // Obtiene el último ID de la tabla 'caca'
                    string Query = "SELECT MAX(id) as LastID FROM synchronizationlog";
                    MySqlCommand lastIdCommand = new MySqlCommand(Query, conexion);
                    //if (lastIdCommand.LastInsertedId == 0)
                    //{
                    //    lastId = 1;
                    //}
                    //else
                    //{
                    lastId = Convert.ToInt32(lastIdCommand.ExecuteScalar());
                    //}

                }
                catch (Exception x)
                {
                    lastId = 0;
                }
                conexion.Close();
            }
            return lastId;
        }

        public static void AddDriver(string name, int idsimpleroute)
        {

            int IdDriver = 0;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    IdDriver = DL.LastIdDrivers() + 1;
                    string Query = "insert into drivers(Id, Name,Id_SimpleRoute,Enable)\r\nvalue('" + IdDriver + "', '" + name + "', '" + idsimpleroute + "','true')";

                    MySqlCommand mySqlData = new MySqlCommand(Query, conexion);
                    //MySqlDataReader reader = mySqlData.ExecuteReader();

                    int rowsAffected = mySqlData.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

        }

        public static bool synchronizationlogExist()
        {
            bool val = false;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    DateTime date = DateTime.Now;
                    string fechaFormateada = date.ToString("yyyy-MM-dd");

                    string Query = "SELECT * FROM synchronizationlog T0  where T0.CreateDate = '" + fechaFormateada + "'";

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        val = true;
                        break;
                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
            return val;
        }

        public static Docnums DocNumExist(string Docnum)
        {
            Docnums doc = new Docnums();
            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT * FROM Docnums T0  where T0.DocNum = '" + Docnum + "'"; 

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        doc.Id = Convert.ToInt32(row["Id"]);
                        doc.DocNum = Convert.ToString(row["DocNum"]);
                        doc.DocDate = Convert.ToString(row["DocDate"]);
                        doc.VisitStatus = Convert.ToString(row["visitStatus"]);
                        doc.Comments = Convert.ToString(row["comments"]);
                        doc.Enable = Convert.ToBoolean(row["Enable"]);
                        doc.Id_ClientAddress = Convert.ToInt32(row["Id_ClientAddress"]);
                        doc.SimpleRoute_Status = Convert.ToBoolean(row["SimpleRoute_Status"]);
                        doc.Id_VisitsimpleRoute = Convert.ToInt32(row["Id_VisitsimpleRoute"]);
                        doc.Code = Convert.ToString(row["Code"]);
                        doc.Id_Driver = Convert.ToInt32(row["Id_Driver"]);

                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
            return doc;
        }

        public static bool EditDocNum(Docnums doc)
        {

            bool validation = false;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    string Query = @"
                                        UPDATE Docnums
                                        SET visitStatus = @visitStatus,
                                        comments = @comments,
                                        SimpleRoute_Status = @SimpleRoute_Status,
                                        Id_VisitsimpleRoute = @Id_VisitsimpleRoute,
                                        Id_Driver = @Id_Driver
                                        WHERE Id = @Id";

                    using (MySqlCommand command = new MySqlCommand(Query, conexion))
                    {
                        // Asignar valores a los parámetros
                        command.Parameters.AddWithValue("@Id", doc.Id);
                        command.Parameters.AddWithValue("@visitStatus", doc.VisitStatus);
                        command.Parameters.AddWithValue("@comments", doc.Comments);
                        command.Parameters.AddWithValue("@SimpleRoute_Status", doc.SimpleRoute_Status);
                        command.Parameters.AddWithValue("@Id_VisitsimpleRoute", doc.Id_VisitsimpleRoute);
                        command.Parameters.AddWithValue("@Id_Driver", doc.Id_Driver);

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

            return validation;

        }

        public static bool EditDocNumByCode(Docnums doc)
        {

            bool validation = false;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    string Query = @"   
                                        UPDATE Docnums
                                        SET visitStatus = @visitStatus,
                                        comments = @comments,
                                        SimpleRoute_Status = @SimpleRoute_Status,
                                        Id_VisitsimpleRoute = @Id_VisitsimpleRoute,
                                        Id_Driver = @Id_Driver
                                        WHERE Code = @Code";

                    using (MySqlCommand command = new MySqlCommand(Query, conexion))
                    {
                        // Asignar valores a los parámetros
                        command.Parameters.AddWithValue("@Id", doc.Id);
                        command.Parameters.AddWithValue("@visitStatus", doc.VisitStatus);
                        command.Parameters.AddWithValue("@comments", doc.Comments);
                        command.Parameters.AddWithValue("@SimpleRoute_Status", doc.SimpleRoute_Status);
                        command.Parameters.AddWithValue("@Id_VisitsimpleRoute", doc.Id_VisitsimpleRoute);
                        command.Parameters.AddWithValue("@Id_Driver", doc.Id_Driver);

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

            return validation;

        }

        public static List<Pendingpackages> pendingpackages()
        {
            Pendingpackages pendingpackages= new Pendingpackages();
            List<Pendingpackages> List = new List<Pendingpackages>();

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {
                    string Query = "SELECT * FROM pendingpackages T0  where T0.Enable = '1'";

                    MySqlDataAdapter mySqlData = new MySqlDataAdapter(Query, conexion);

                    DataTable data = new DataTable();
                    mySqlData.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        pendingpackages.Id = Convert.ToInt32(row["Id"]);
                        pendingpackages.DocNum = Convert.ToString(row["Docnum"]);
                        pendingpackages.Enable = Convert.ToBoolean(row["Enable"]);
                        pendingpackages.Id_DocNums = Convert.ToInt32(row["Id_Docnums"]);

                        List.Add(pendingpackages);
                        pendingpackages = new Pendingpackages();
                    }
                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }
            return List;
        }

        public static bool Disablependingpackages(int id)
        {
            bool val = false;

            using (MySqlConnection conexion = OpenConnectionMysql())
            {
                try
                {

                    string Query = " UPDATE pendingpackages SET Enable = 0 WHERE (Id = '" + id + "')";


                    using (MySqlCommand command = new MySqlCommand(Query, conexion))
                    {

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        // Comprobar si la actualización fue exitosa
                        if (rowsAffected > 0)
                        {
                            val = true;
                        }
                    }

                }
                catch (Exception x)
                {
                }
                conexion.Close();
            }

            return val;
        }

        public static void Dopendingpackages()
        {
            bool val = false;
            try
            {
                List<Pendingpackages> List = DL.pendingpackages();

                if (List.Count > 0)
                {
                    using (MySqlConnection conexion = DL.OpenConnectionMysql())
                    {
                        foreach (var L in List)
                        {
                            dynamic jsonObject = DL.PackingList(L.DocNum);

                            jsonObject = JsonConvert.DeserializeObject<JObject>(jsonObject);

                            //si paking esta vacio  agregar a PendingPackages y regresar true.
                            string Docnum = jsonObject["message"]["DocNum"];

                            if (Docnum == null)
                            {
                                //Nada
                            }
                            else
                            {
                                // Acceder a los valores
                                var details = jsonObject["message"]["Details"];

                                foreach (var detail in details)
                                {
                                    int LastIdPackage = DL.LastIdPackages() + 1;

                                    string Query = "insert into Packages(Id, NamePackage,Lenght,Width,Height,Volumetric,Weight,quantityItems,Id_Docnums)\r\nvalue('" + LastIdPackage + "', '" + detail["selectPackage"]["Name"] + "','" + detail["selectPackage"]["Lenght"] + "','" + detail["selectPackage"]["Width"] + "','" + detail["selectPackage"]["Height"] + "','" + detail["selectPackage"]["Volumetric"] + "','" + detail["selectPackage"]["Weight"] + "','" + detail["quantityItems"] + "','" + L.Id_DocNums + "');";

                                    MySqlCommand mySqlData = new MySqlCommand(Query, conexion);
                                    //MySqlDataReader reader = mySqlData.ExecuteReader();

                                    int rowsAffected = mySqlData.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        var items = detail["items"];
                                        foreach (var item in items)
                                        {
                                            int Packagedetails = DL.LastIdPackagedetails() + 1;
                                            string QueryTwo = "insert into Packagedetails(Id, ItemCode,Sku,Weight,Quantity,ItemName,Id_Packages)\r\nvalue('" + Packagedetails + "', '" + item["ItemCode"] + "', '" + item["Sku"] + "','" + item["Weight"] + "','" + item["Quantity"] + "','" + item["ItemName"] + "','" + LastIdPackage + "')";

                                            MySqlCommand mySqlDataTwo = new MySqlCommand(QueryTwo, conexion);
                                            //MySqlDataReader reader = mySqlData.ExecuteReader();

                                            int rowsAffectedTwo = mySqlDataTwo.ExecuteNonQuery();

                                            if (rowsAffectedTwo > 0)
                                            {
                                                val = true;
                                            }

                                        }
                                    }

                                    if (val)
                                    {
                                        _ = DL.Disablependingpackages(L.Id);
                                    }

                                }
                            }

                        }
                        conexion.Close();
                    }
                }

            }
            catch (Exception d)
            {

            }

        }
    }
}