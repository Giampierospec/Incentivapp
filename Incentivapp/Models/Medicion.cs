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
    using System.ComponentModel.DataAnnotations;

    public partial class Medicion
    {
        public int idMedicion { get; set; }
        [Required(ErrorMessage="El rango es requerido")]
        public Nullable<int> rango { get; set; }
        public Nullable<int> idPremio { get; set; }
        public Nullable<int> idUser { get; set; }
        public Nullable<int> createdBy { get; set; }
    
        public virtual Premio Premio { get; set; }
        public virtual Rango Rango1 { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Usuario Usuario1 { get; set; }
    }
}
