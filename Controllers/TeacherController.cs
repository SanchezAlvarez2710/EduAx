using EduAx.Models;
using EduAx.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace EduAx.Controllers
{
    public class TeacherController : Controller
    {
        eduAxDatabaseEntities db = new eduAxDatabaseEntities();
        // GET: Teacher
        public ActionResult Index()
        {
            Global oGlobal = new Global();
            var oUser = (EduAx.Models.PERSON)Session["user"];
            var oGroups = (from p in db.PERSON
                            join t in db.TEACHER on p.ID_PERSON equals t.ID_TEACHER
                            where oUser.ID_PERSON == t.ID_TEACHER
                            select p).ToList();
            oGlobal.name_person = oGroups.ElementAt(0).NAME_PERSON;
            oGlobal.groups_count = oGroups.Count();
            var oStudents = (from t in db.TEACHER
                             join sc in db.STUDENT_COURSE on t.ID_GROUP equals sc.ID_GROUP                             
                             select t).Count();///FALTAAA
            return View(oGlobal);
        }
    }
}
