using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EduAx.Models;
using EduAx.Models.ViewModel;
using System.Data;
using Microsoft.Ajax.Utilities;
using System.Runtime.CompilerServices;
using System.Data.Entity.Core;

namespace EduAx.Controllers
{
    public class HomeController : Controller
    {
        private eduAxEntities db = new eduAxEntities();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
             Session["usuario"] = null;
             return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(Person oUsuario)
        {
            oUsuario.password = ConvertirSha256(oUsuario.password);
            IEnumerable<EduAx.Models.ViewModel.Person> acceso = from o in db.PERSON
                                                                where o.email == oUsuario.email && o.password == oUsuario.password
                                                                select new Person
                                                                {
                                                                    id_person = (int)o.id_person,
                                                                    email = o.email,
                                                                    password = o.password,
                                                                    rol = o.rol
                                                                };
            if (acceso.Any())
            {

                var condicion = acceso.ElementAt(0).rol.ToLower();
                oUsuario.rol = condicion;

                Session["usuario"] = oUsuario;
                return RedirectToAction("Index", "Home");

            }
            ViewData["Mensaje"] = "Usuario o contraseña incorrectos";
            return View();
        }

        public static string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

    }
}