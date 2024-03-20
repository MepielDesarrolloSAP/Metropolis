using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Gate.Clases;
using Gate.Components.BL;
using Gate.Components.DL;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Gate.Controllers
{
    public class AuthController : Controller
    {


        public ActionResult Signin()
        {
            return View();
        }


        public ActionResult Signup()
        {
            return View();
        }


        public ActionResult Recoverpw()
        {
            return View();
        }


        public JsonResult ValidaLogin(string UsuarioPar, string PswPar)
        {
            Users objeto = DL.FindUser(UsuarioPar, PswPar);//new BL().FindUser(UsuarioPar, PswPar);
            if (objeto.Username != null)
            {
                if (objeto.Enable == true)
                {
                    if (objeto.Id_Role == 1)
                    {
                        FormsAuthentication.SetAuthCookie(objeto.Username, false);
                        Session["Usuario"] = objeto;
                    }
                    else
                    {
                        DL.DisableUser(objeto.Username);
                        //Consultas.TrueSL(objeto.UserName);
                        FormsAuthentication.SetAuthCookie(objeto.Username, false);
                        Session["Usuario"] = objeto;
                        // Crear y ejecutar una tarea en segundo plano
                        Task.Run(() =>
                        {
                            //Inicia
                            Task.Delay(120000).Wait();

                            DL.EnableUser(objeto.Username);
                            //Finaliza
                        });
                    }
                }
                else
                {
                    Users a = new Users();
                    a.Enable = false;
                    return Json(a);
                }
            }
            return Json(objeto);
        }


        public JsonResult Recover(string UsuarioPar)
        {
            Users objeto = DL.FindUserTwo(UsuarioPar);
            return Json(objeto);
        }


        public RedirectToRouteResult ValidaRol()
        {
            try 
            {
                Users User = System.Web.HttpContext.Current.Session["Usuario"] as Users;
                bool val = false;

                switch (User.Id_Role)
                {
                    case 1:

                        #region SINCRONIZACION

                        Task.Run(() =>
                        {
                            DL.Dopendingpackages();

                            //Validar si existe una sincronizacion con fecha del dia de hoy
                            val = DL.synchronizationlogExist();

                            if (val)
                            {
                                //nada
                            }

                            else
                            {
                                _ = DL.Drivers();
                                _ = DL.Visits();

                                //Crear log
                                DL.Addsynchronizationlog();
                            }

                        });

                        #endregion

                        return RedirectToAction("Index", "Home"/*, new { User = User.UserName }*/);

                    case 2:
                        return RedirectToAction("Index", "Home"/*, new { User = User.UserName }*/);
                    case 10:
                        return RedirectToAction("Index", "Home"/*, new { User = User.UserName }*/);
                }

            }
            catch(Exception c)
            {
                return RedirectToAction("Signin", "Auth");
            }
            return RedirectToAction("Signin", "Auth");
        }


        public RedirectToRouteResult LogOut() //System.Web.Mvc.RedirectToRouteResult LogOut()
        {
            try
            {
                Users User = System.Web.HttpContext.Current.Session["Usuario"] as Users;

                if (User != null)
                {
                    if (User.Id_Role == 1)
                    {
                        return RedirectToAction("Signin", "Auth");
                    }

                    else
                    {
                        DL.EnableUser(User.Username);
                        FormsAuthentication.SignOut();
                        Session["Usuario"] = "";
                        return RedirectToAction("Signin", "Auth");
                    }

                }
                else
                {
                    return RedirectToAction("Signin", "Auth");
                }

            }
            catch (Exception x)
            {
                return RedirectToAction("Signin", "Auth");
            }

        }

    }
}