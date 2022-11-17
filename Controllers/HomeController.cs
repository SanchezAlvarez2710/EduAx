using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using EduAx.Models;
using System.Data;

namespace EduAx.Controllers
{
    public class HomeController : Controller
    {
        private eduAxDatabaseEntities db = new eduAxDatabaseEntities();
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
             Session["user"] = null;
             return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Login(PERSON oUser)
        {
            oUser.PASS_PERSON = ConvertToSha256(oUser.PASS_PERSON);
            List<PERSON> access = (from P in db.PERSON
                                         //join ADM in db.ADMIN on P.ID_PERSON equals ADM.ID_ADMIN
                                         where P.EMAIL_PERSON == oUser.EMAIL_PERSON && P.PASS_PERSON == oUser.PASS_PERSON
                                         select new
                                         {
                                            ID_PERSON = (int)P.ID_PERSON,
                                            EMAIL_PERSON = P.EMAIL_PERSON,
                                            PASS_PERSON = P.PASS_PERSON,
                                            ROLE_PERSON = P.ROLE_PERSON,
                                            AVATAR_PERSON = P.AVATAR_PERSON
                                         }).AsEnumerable().Select(x => new PERSON
                                         {
                                             ID_PERSON = (int)x.ID_PERSON,
                                             EMAIL_PERSON = x.EMAIL_PERSON,
                                             PASS_PERSON = x.PASS_PERSON,
                                             ROLE_PERSON = x.ROLE_PERSON,
                                             AVATAR_PERSON = x.AVATAR_PERSON
                                         }).ToList();
            if (access.Any())
            {
                var condicion = access.ElementAt(0).ROLE_PERSON.ToLower();
                oUser.ROLE_PERSON = condicion;
                Session["user"] = oUser;
                return RedirectToAction("Index", "Home");

            }
            ViewData["Message"] = "Incorrect password or user";
            return View();
        }
        public static string ConvertToSha256(string text)
        {            
            //USAR LA REFERENCIA DE "System.Security.Cryptography"
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(text));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }

    }
}