﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EduAx.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class eduAxDatabaseEntities : DbContext
    {
        public eduAxDatabaseEntities()
            : base("name=eduAxDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADMIN> ADMIN { get; set; }
        public virtual DbSet<COURSE> COURSE { get; set; }
        public virtual DbSet<GROUP> GROUP { get; set; }
        public virtual DbSet<GROUP_COURSE> GROUP_COURSE { get; set; }
        public virtual DbSet<PERSON> PERSON { get; set; }
        public virtual DbSet<STUDENT> STUDENT { get; set; }
        public virtual DbSet<STUDENT_COURSE> STUDENT_COURSE { get; set; }
        public virtual DbSet<TEACHER> TEACHER { get; set; }
    }
}
