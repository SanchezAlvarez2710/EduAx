//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class COURSE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COURSE()
        {
            this.STUDENT_COURSE = new HashSet<STUDENT_COURSE>();
            this.GROUP = new HashSet<GROUP>();
        }
    
        public decimal ID_COURSE { get; set; }
        public string NAME_COURSE { get; set; }
        public string TYPE_COURSE { get; set; }
        public string INFO_COURSE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_COURSE> STUDENT_COURSE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GROUP> GROUP { get; set; }
    }
}
