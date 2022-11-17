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
        public ActionResult Dash(int id_course)
        {
            List<COURSE> oCourse = (from c in db.COURSE
                                    where c.ID_COURSE == id_course
                                    select new
                                    {
                                        ID_COURSE = (int)id_course,
                                        NAME_COURSE = c.NAME_COURSE,
                                        TYPE_COURSE = c.TYPE_COURSE,
                                        INFO_COURSE = c.INFO_COURSE,
                                        ICON_COURSE = c.ICON_COURSE                                        
                                    }).AsEnumerable().Select(x => new COURSE
                                    {
                                        ID_COURSE = (int)x.ID_COURSE,
                                        NAME_COURSE = x.NAME_COURSE,
                                        TYPE_COURSE = x.TYPE_COURSE,
                                        INFO_COURSE = x.INFO_COURSE,
                                        ICON_COURSE = x.ICON_COURSE
                                    }).ToList();
            return View(oCourse);
        }
    }
}