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
            var oUser = (EduAx.Models.PERSON)Session["user"];
            var oGroups = (from p in db.PERSON
                            join t in db.TEACHER on p.ID_PERSON equals t.ID_TEACHER
                            where oUser.ID_PERSON == t.ID_TEACHER
                            select p).ToList();            
            var oStudents = (from t in db.TEACHER
                             join sc in db.STUDENT_COURSE on t.ID_GROUP equals sc.ID_GROUP                             
                             select t).Count();
            var oCourses = (from gc in db.GROUP_COURSE
                            join t in db.TEACHER on gc.ID_GROUP equals t.ID_GROUP
                            where t.ID_TEACHER == oUser.ID_PERSON
                            select gc).ToList();
            //ELIMINA LOS CURSOS REPETIDOS
            for(int i = 0; i < oCourses.Count; i++)
            {
                for(int j = i + 1; j < oCourses.Count(); j++)
                {
                    if(oCourses.ElementAt(j).ID_COURSE == oCourses.ElementAt(i).ID_COURSE)
                    {
                        oCourses.RemoveAt(j);
                    }
                }
            }
            //IMPORTA LOS CURSOS RELACIONADOS CON EL PROFESOR
            var oGlobal = (from gc in db.GROUP_COURSE
                           join t in db.TEACHER on gc.ID_GROUP equals t.ID_GROUP
                           where t.ID_TEACHER == oUser.ID_PERSON
                           select new Global
                           {
                               id_course = (int)gc.ID_COURSE,
                               id_group = (int)gc.ID_GROUP,
                               places_groupcourse = (int)gc.PLACES_GROUPCOURSE,
                               state_groupcourse = gc.STATE_GROUPCOURSE
                           }).ToList();  
            oGlobal.Add(new Global
            {
                name_person = oGroups.ElementAt(0).NAME_PERSON,
                groups_count = oGroups.Count,
                students_count = oStudents,
                courses_count = oCourses.Count()
            });
            return View(oGlobal);
        }

        public ActionResult Dash(int id_group, string state_group)
        {
            var oStudents = (from sc in db.STUDENT_COURSE                             
                             where sc.ID_GROUP == id_group
                             select new Global
                             {
                                 id_group = id_group,
                                 id_student = (int)sc.ID_STUDENT,
                                 stgrade = sc.STGRADE_STUDENTCOURSE,
                                 ndgrade = sc.NDGRADE_STUDENTCOURSE,
                                 rdgrade = sc.RDGRADE_STUDENTCOURSE,
                                 thgrade = sc.THGRADE_STUDENTCOURSE,
                                 times_studentcourse = (int)sc.TIMES_STUDENTCOURSE,
                                 state_student = sc.STATE_STUDENTCOURSE,
                                 state_groupcourse = state_group
                             }).ToList();
            if (!(oStudents.Count > 0))
            {
                var oGroup = (from gc in db.GROUP_COURSE
                              where gc.ID_GROUP == id_group
                              select new Global
                              {
                                  id_group = (int)gc.ID_GROUP,
                                  state_groupcourse = gc.STATE_GROUPCOURSE,
                              }).ToList();
                return View(oGroup);
            }
            return View(oStudents);
        }
    }
}
