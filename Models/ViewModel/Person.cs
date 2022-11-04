using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduAx.Models.ViewModel
{
    public class Person
    {
        public int id_person { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string rol { get; set; }
    }
}