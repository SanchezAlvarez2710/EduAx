using EduAx.Models;
using EduAx.Models.ViewModel;
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
        //public ActionResult Dash(int id_course)
        //{
        //    List<COURSE> oCourse = (from c in db.COURSE
        //                            where c.ID_COURSE == id_course
        //                            select new
        //                            {
        //                                ID_COURSE = (int)id_course,
        //                                NAME_COURSE = c.NAME_COURSE,
        //                                TYPE_COURSE = c.TYPE_COURSE,
        //                                INFO_COURSE = c.INFO_COURSE,
        //                                ICON_COURSE = c.ICON_COURSE                                        
        //                            }).AsEnumerable().Select(x => new COURSE
        //                            {
        //                                ID_COURSE = (int)x.ID_COURSE,
        //                                NAME_COURSE = x.NAME_COURSE,
        //                                TYPE_COURSE = x.TYPE_COURSE,
        //                                INFO_COURSE = x.INFO_COURSE,
        //                                ICON_COURSE = x.ICON_COURSE
        //                            }).ToList();
        //    return View(oCourse);
        //}
        [HttpGet]
        public ActionResult Dash(int id_course)
        {
         
            var user = (PERSON)HttpContext.Session["user"];

            var oCourse_Student = (from c in db.COURSE
                                   join p in db.STUDENT_COURSE on c.ID_COURSE equals p.ID_COURSE 
                                   join o in db.GROUP_COURSE on c.ID_COURSE  equals o.ID_COURSE
                                   join g in db.GROUP on o.ID_GROUP equals g.ID_GROUP
                                   join s in db.STUDENT_GROUP on g.ID_GROUP equals s.ID_GROUP
                                   join t in db.TEACHER on g.ID_GROUP equals t.ID_GROUP
                                   where c.ID_COURSE == id_course && s.ID_STUDENT == user.ID_PERSON && p.ID_STUDENT == user.ID_PERSON
                                   select new course_student
                                   {
                                       id_course = (int)c.ID_COURSE,
                                       name_course = c.NAME_COURSE,
                                       type_course = c.TYPE_COURSE,
                                       info_course = c.INFO_COURSE,
                                       icon_course = c.ICON_COURSE,
                                       image_course = c.IMAGE_COURSE,
                                       id_teacher = (int)t.ID_TEACHER,
                                       id_group = (int)s.ID_GROUP,
                                       places_groupcourse = (int)o.PLACES_GROUPCOURSE,
                                       id_student = (int)s.ID_STUDENT,
                                       stgrade = (int)p.STGRADE_STUDENTCOURSE,
                                       ndgrade = (int)p.NDGRADE_STUDENTCOURSE,
                                       rdgrade = (int)p.RDGRADE_STUDENTCOURSE,
                                       thgrade = (int)p.THGRADE_STUDENTCOURSE,
                                       state_student = p.STATE_STUDENTCOURSE
                                       
                                   });

            return View(oCourse_Student);
        }
    }
}