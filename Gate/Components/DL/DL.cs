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

namespace Gate.Components.DL
{
    public class DL
    {
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
                        user.Name = Convert.ToString(row["Name"]);
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
                        user.Name = Convert.ToString(row["Name"]);
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

        public static int AddFolioRoute(int Id_typeofroute)
        {
            int folio = 0;

            int FolioRoute = LastIdAFolioRoute() + 1;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {
                    DateTime date = DateTime.Now;
                    string fechaFormateada = date.ToString("yyyy-MM-dd");

                    string Query = "insert into FolioRoute(Id, Folio,CreateDate,Driver,Id_typeofroute,Enable)\r\nvalue('" + FolioRoute + "', '" + FolioRoute + "','"+ fechaFormateada + "','','" + Id_typeofroute + "','" + true + "');";

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
                    string Query = "SELECT T0.Id FROM Clients T0 where  T0.CardCode = 'CardCode'"; // where users.Enable = 0";

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

        public static int AddClient(string CardName, string CardCode)
        {
            int folio = 0;

            int FolioClient = LastIdClient() + 1;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {

                    string Query = "insert into Clients(Id, CardName,CardCode)\r\nvalue('" + FolioClient + "', '" + CardName + "','" + CardCode + "');";

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

        public static bool AddRoute(string U_NAME, string Conditions, string ConditionsType, string DocNums, string Comments, string Phone, bool enable, int Id_Users, int Id_FolioRoute, int Id_clients)
        {
            bool val = false;

            int idroute = DL.LastIdRoute() + 1;

            using (MySqlConnection conexion = DL.OpenConnectionMysql())
            {
                try
                {

                    string Query = "insert into Route(Id, U_NAME,Conditions,ConditionsType,DocNums,Comments,Phone,Id_Users,Id_FolioRoute,Id_Clients)\r\nvalue('" + idroute + "', '" + U_NAME + "', '" + Conditions + "', '" + ConditionsType + "','" + DocNums + "','" + Comments + "','" + Phone + "', '" + enable + "', '" + Id_Users + "', '" + Id_FolioRoute + "', '" + Id_clients + "')";

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

                    string Query = "insert into ClientAddress(Id, ShipToCode,Street,Colony,ZipCode,City,Id_Clients)\r\nvalue('" + IdClientAddress + "', '" + ShipToCode + "', '" + Street + "', '" + Colony + "','" + ZipCode + "','" + City + "','" + Id_clients + "')";

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

                    foreach (string registro in registros)
                    {
                        IdDocNums = DL.LastIdDocNums() + 1;
                        string Query = "insert into Docnums(Id, DocNum,DocDate,visitStatus,comments,Id_ClientAddress\r\n,Id_Clients)\r\nvalue('" + IdDocNums + "', '" + registro + "', '" + fechaFormateada + "','','','" + IdClientAddress + "')";

                        MySqlCommand mySqlData = new MySqlCommand(Query, conexion);
                        //MySqlDataReader reader = mySqlData.ExecuteReader();

                        int rowsAffected = mySqlData.ExecuteNonQuery();

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
                return val;
            }

        }




    }
}