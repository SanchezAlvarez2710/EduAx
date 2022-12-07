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
        public ActionResult Dash(int id_course)
        {
            var oUser = (EduAx.Models.PERSON)Session["user"];
            //GENERAL COURSES INFO SHOWED TO ALL USERS
            List<Global> oCourse = (from c in db.COURSE
                                          join gc in db.GROUP_COURSE on c.ID_COURSE equals gc.ID_COURSE                                      
                                          where c.ID_COURSE == id_course
                                          select new Global
                                          {
                                              id_course = (int)id_course,
                                              name_course = c.NAME_COURSE,
                                              type_course = c.TYPE_COURSE,
                                              info_course = c.INFO_COURSE,
                                              icon_course = c.ICON_COURSE,
                                              image_course = c.IMAGE_COURSE,
                                              id_group = (int)gc.ID_GROUP,
                                              places_groupcourse = (int)gc.PLACES_GROUPCOURSE,
                                              groups_count = (from gc in db.GROUP_COURSE
                                                              where gc.ID_COURSE == id_course
                                                              select gc).Count(),                                             
                                          }).ToList();
            for(int i = 1; i < oCourse.Count; i++)
            {
                oCourse.ElementAt(0).places_groupcourse += oCourse.ElementAt(i).places_groupcourse;
                //oCourse.RemoveAt(i);
            }                        
            if (oUser != null)
            {
                switch (oUser.ROLE_PERSON.ToLower())
                {
                    case "student":
                        List<Global> oStudent = (from sc in db.STUDENT_COURSE
                                  where sc.ID_STUDENT == oUser.ID_PERSON && sc.ID_COURSE == id_course
                                  select new Global
                                  {
                                      course_count = (int)sc.TIMES_STUDENTCOURSE,
                                      state_student = sc.STATE_STUDENTCOURSE
                                  }).ToList();
                        if (oStudent.Count() > 0)
                        {
                            oCourse.ElementAt(0).course_count = (int)oStudent.ElementAt(0).course_count;
                            oCourse.ElementAt(0).state_student = oStudent.ElementAt(0).state_student;
                        } else
                        {
                            oCourse.ElementAt(0).course_count = 0;
                            oCourse.ElementAt(0).state_student = "TO DO";
                        }
                        break;
                    case "teacher":

                        break;
                    case "admin":

                        break;
                }
            }

            return View(oCourse);
        }
    }
}