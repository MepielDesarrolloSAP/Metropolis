using Gate.Clases;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Gate.Properties;


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

        //Obtener ultimo Id agregado en usuarios
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

        //Obtener ultimo Id agregado en usuarios
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

                    string Query = "insert into FolioRoute(Id, Folio,CreateDate,Id_typeofroute)\r\nvalue('" + FolioRoute + "', '" + FolioRoute + "','"+ fechaFormateada + "','"+ Id_typeofroute + "');";

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


    }
}