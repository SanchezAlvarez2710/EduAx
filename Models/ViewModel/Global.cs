using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduAx.Models.ViewModel
{
    public class Global
    {
        //COURSE
        public int id_course { get; set; }
        public string name_course { get; set; }
        public string type_course { get; set; }
        public string info_course { get; set; }
        public string icon_course { get; set; }
        public string image_course { get; set; }
        //GROUP
        public int id_group { get; set; }
        //GROUP_COURSE
        public int places_groupcourse { get; set; }
        public string state_groupcourse { get; set; }
        //STUDENT_COURSE
        public int id_student { get; set; }     //STUDENT
        public float stgrade { get; set; }
        public float ndgrade { get; set; }
        public float rdgrade { get; set; }
        public float thgrade { get; set; }
        public string state_student { get; set; }
       //TEACHER
        public int id_teacher { get; set; }
        //PERSON
        public int id_person { get; set; }
        public string name_person { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
        public string role { get; set; }
        public string avatar { get; set; }
        //ADMIN
        public int id_admin { get; set; }
        //FLAG COURSES VIEW CONDITION
        public int times_studentcourse { get; set; }
        //GROUPS COUNT
        public int groups_count { get; set; }
        //STUDENTS COUNT
        public int students_count { get; set; }
        //COURSES COUNT
        public int courses_count { get; set; }
        //TO DO COUNT
        public int todo_count { get; set; }
        //DONE COUNT
        public int done_count { get; set; }        
        //COURSES VIEWED
        public int viewed_count { get; set; }
        //DOING COUNT
        public int doing_count { get; set; }
        //TEACHERS COUNT
        public int teachers_count { get; set; }
    }
}