using EduAx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduAx.Models.ViewModel;

namespace EduAx.Controllers
{
    public class TrayectoryController : Controller
    {
        eduAxDatabaseEntities db = new eduAxDatabaseEntities();
        // GET: Trayectory
        public ActionResult Index()
        {
            var oUser = (EduAx.Models.PERSON)Session["user"];
            if (oUser != null)
            {
                List<EduAx.Models.ViewModel.Global> oCourses = (from sc in db.STUDENT_COURSE
                                                                join c in db.COURSE on sc.ID_COURSE equals c.ID_COURSE
                                                                join gc in db.GROUP_COURSE on sc.ID_GROUP equals gc.ID_GROUP
                                                                where oUser.ID_PERSON == sc.ID_STUDENT
                                                                select new Global
                                                                {
                                                                    id_student = (int)sc.ID_STUDENT,
                                                                    id_course = (int)sc.ID_COURSE,
                                                                    icon_course = c.ICON_COURSE,
                                                                    name_course = c.NAME_COURSE,
                                                                    id_group = (int)sc.ID_GROUP,
                                                                    stgrade = (int)sc.STGRADE_STUDENTCOURSE,
                                                                    ndgrade = (int)sc.NDGRADE_STUDENTCOURSE,
                                                                    rdgrade = (int)sc.RDGRADE_STUDENTCOURSE,
                                                                    thgrade = (int)sc.THGRADE_STUDENTCOURSE,
                                                                    times_studentcourse = (int)sc.TIMES_STUDENTCOURSE,
                                                                    state_student = sc.STATE_STUDENTCOURSE,
                                                                    state_groupcourse = gc.STATE_GROUPCOURSE                                                                    
                                                                }).ToList();
                EduAx.Models.ViewModel.Global oStudent = new Global
                {
                    done_count = (from sc in db.STUDENT_COURSE
                                  where oUser.ID_PERSON == sc.ID_STUDENT &&
                                        sc.STATE_STUDENTCOURSE.ToLower() == "done"
                                  select sc).Count(),
                    viewed_count = (from sc in db.STUDENT_COURSE
                                    where oUser.ID_PERSON == sc.ID_STUDENT &&
                                          sc.STATE_STUDENTCOURSE.ToLower() == "cancelled" ||
                                          sc.STATE_STUDENTCOURSE.ToLower() == "flunked" ||
                                          sc.STATE_STUDENTCOURSE.ToLower() == "done"
                                    select sc).Count(),
                    todo_count = (from c in db.COURSE
                                  select c).Count(),
                    doing_count = (from sc in db.STUDENT_COURSE
                                   where oUser.ID_PERSON == sc.ID_STUDENT &&
                                         sc.STATE_STUDENTCOURSE.ToLower() == "doing"
                                   select sc).Count()
                };
                oStudent.todo_count -= oStudent.viewed_count + oStudent.done_count + oStudent.doing_count;
                oCourses.Add(oStudent);
                return View(oCourses);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}