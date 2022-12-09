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
            //List<List<EduAx.Models.ViewModel.Global>> oGlobal = new List<List<EduAx.Models.ViewModel.Global>>();   
            List<EduAx.Models.ViewModel.Global> oStudent = (from sc in db.STUDENT_COURSE
                                                            join c in db.COURSE on sc.ID_COURSE equals c.ID_COURSE
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
                                                              state_student = sc.STATE_STUDENTCOURSE
                                                          }).ToList();
            string[] oCourses = {"FIRST AID BASICS", "RCP TECHNIQUES", "CLINIC REPORTS", "PHARMACOLOGY"};            
            
            //oGlobal.Add(oStudent);

            return View(oStudent);
        }
    }
}