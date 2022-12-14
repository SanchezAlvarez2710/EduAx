using System;
using System.Collections.Generic;
using System.Linq;
using EduAx.Models;
using EduAx.Models.ViewModel;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;

namespace EduAx.Controllers
{
    public class AdminController : Controller
    {
        eduAxDatabaseEntities db = new eduAxDatabaseEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                var oUser = (EduAx.Models.PERSON)Session["user"];

                var oStudents = (from t in db.PERSON
                                 where t.ROLE_PERSON.Equals("STUDENT")
                                 select t).Count();
                var oCourses = (from t in db.COURSE
                                select t).Count();
                var oTeachers = (from t in db.PERSON
                                 where t.ROLE_PERSON.Equals("TEACHER")
                                 select t).Count();

                var oGlobal = new Global
                {
                    name_person = oUser.NAME_PERSON,
                    teachers_count = oTeachers,
                    students_count = oStudents,
                    courses_count = oCourses
                };
                return View(oGlobal);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}

