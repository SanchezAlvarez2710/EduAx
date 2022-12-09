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

        //GET: Student-Course-Teacher        
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
                                        places_groupcourse = (int)gc.PLACES_GROUPCOURSE,
                                        groups_count = (from gc in db.GROUP_COURSE
                                                        where gc.ID_COURSE == id_course
                                                        select gc).Count(),
                                    }).ToList();
            var count = oCourse.Count();
            for (int i = 1; i < count; i++)
            {
                oCourse.ElementAt(0).places_groupcourse += oCourse.ElementAt(1).places_groupcourse;
                oCourse.RemoveAt(1);
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
                                                     id_group = (int)sc.ID_GROUP,
                                                     stgrade = (float)sc.STGRADE_STUDENTCOURSE,
                                                     ndgrade = (float)sc.NDGRADE_STUDENTCOURSE,
                                                     rdgrade = (float)sc.RDGRADE_STUDENTCOURSE,
                                                     thgrade = (float)sc.THGRADE_STUDENTCOURSE,
                                                     times_studentcourse = (int)sc.TIMES_STUDENTCOURSE,
                                                     state_student = sc.STATE_STUDENTCOURSE
                                                 }).ToList();
                        if (oStudent.Count() > 0)
                        {
                            var oId_group = (decimal)oStudent.ElementAt(0).id_group;
                            if (oStudent.ElementAt(0).state_student.ToLower() == "doing" ||
                                oStudent.ElementAt(0).state_student.ToLower() == "to do")
                            {
                                List<Global> oTeacher = (from p in db.PERSON
                                                         join t in db.TEACHER on p.ID_PERSON equals t.ID_TEACHER
                                                         where oId_group == t.ID_GROUP
                                                         select new Global
                                                         {
                                                             name_person = p.NAME_PERSON
                                                         }).ToList();
                                oCourse.ElementAt(0).name_person = oTeacher.ElementAt(0).name_person;
                            }
                            oCourse.ElementAt(0).id_group = oStudent.ElementAt(0).id_group;
                            oCourse.ElementAt(0).stgrade = oStudent.ElementAt(0).stgrade;
                            oCourse.ElementAt(0).ndgrade = oStudent.ElementAt(0).ndgrade;
                            oCourse.ElementAt(0).rdgrade = oStudent.ElementAt(0).rdgrade;
                            oCourse.ElementAt(0).thgrade = oStudent.ElementAt(0).thgrade;
                            oCourse.ElementAt(0).times_studentcourse = (int)oStudent.ElementAt(0).times_studentcourse;
                            oCourse.ElementAt(0).state_student = oStudent.ElementAt(0).state_student;
                        }
                        else
                        {
                            oCourse.ElementAt(0).name_person = "UNSIGNED";
                            oCourse.ElementAt(0).times_studentcourse = 0;
                            oCourse.ElementAt(0).state_student = "TO DO";
                        }
                        List<Global> oGroup = (from p in db.PERSON
                                               join t in db.TEACHER on p.ID_PERSON equals t.ID_TEACHER
                                               join gc in db.GROUP_COURSE on t.ID_GROUP equals gc.ID_GROUP
                                               where id_course == gc.ID_COURSE
                                               select new Global
                                               {
                                                   id_group = (int)t.ID_GROUP,
                                                   name_person = p.NAME_PERSON,
                                                   email = p.EMAIL_PERSON,
                                                   places_groupcourse = (int)gc.PLACES_GROUPCOURSE
                                               }).ToList();
                        for (int i = 0; i < oGroup.Count; i++)
                        {
                            oCourse.Add(oGroup.ElementAt(i));
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

        //GET: Enrolling course as student                
        public ActionResult Enroll(int id_course, int id_group)
        {
            var oUser = (EduAx.Models.PERSON)Session["user"];
            var oStudent = (from sc in db.STUDENT_COURSE
                            where oUser.ID_PERSON == sc.ID_STUDENT && sc.ID_COURSE == id_course
                            select sc).ToList();
            if(oStudent.Count > 0)
            {
                db.STUDENT_COURSE.Find(oStudent.ElementAt(0).ID_STUDENTCOURSE).ID_GROUP = id_group;
                db.STUDENT_COURSE.Find(oStudent.ElementAt(0).ID_STUDENTCOURSE).STGRADE_STUDENTCOURSE = 0;
                db.STUDENT_COURSE.Find(oStudent.ElementAt(0).ID_STUDENTCOURSE).NDGRADE_STUDENTCOURSE = 0;
                db.STUDENT_COURSE.Find(oStudent.ElementAt(0).ID_STUDENTCOURSE).RDGRADE_STUDENTCOURSE = 0;
                db.STUDENT_COURSE.Find(oStudent.ElementAt(0).ID_STUDENTCOURSE).THGRADE_STUDENTCOURSE = 0;
                db.STUDENT_COURSE.Find(oStudent.ElementAt(0).ID_STUDENTCOURSE).TIMES_STUDENTCOURSE =
                    db.STUDENT_COURSE.Find(oStudent.ElementAt(0).ID_STUDENTCOURSE).TIMES_STUDENTCOURSE - 1;
                db.STUDENT_COURSE.Find(oStudent.ElementAt(0).ID_STUDENTCOURSE).STATE_STUDENTCOURSE = "DOING";
                db.SaveChanges();
            } else
            {
                STUDENT_COURSE newStudent = new STUDENT_COURSE();
                newStudent.ID_STUDENT = oUser.ID_PERSON;
                newStudent.ID_COURSE = id_course;
                newStudent.ID_GROUP = id_group;
                newStudent.TIMES_STUDENTCOURSE = 1;
                newStudent.STATE_STUDENTCOURSE = "DOING";
                db.STUDENT_COURSE.Add(newStudent);
                db.SaveChanges();
            }            
            return RedirectToAction("Dash", new { id_course = id_course });
        }
    }
}