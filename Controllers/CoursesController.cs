using EduAx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduAx.Controllers
{
    public class CoursesController : Controller
    {
        private eduAxDatabaseEntities db = new eduAxDatabaseEntities();
        // GET: Courses
        public ActionResult Index()
        {
            EduAx.Models.COURSE oCourse = new COURSE();
            IEnumerable<COURSE> cOURSES = (from c in db.COURSE
                                          select c);
            return View(cOURSES);                                          
        }
        // GET: Student
        public ActionResult Dash()
        {
            return View();
        }
    }
}