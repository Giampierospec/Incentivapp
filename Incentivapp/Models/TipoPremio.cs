//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Incentivapp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipoPremio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoPremio()
        {
            this.Premios = new HashSet<Premio>();
        }
    
        public int idTipoPremio { get; set; }
        public string tipo { get; set; }
        public Nullable<int> idUser { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Premio> Premios { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
