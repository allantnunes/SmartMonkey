//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartMonkey
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class Instituicao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Instituicao()
        {
            this.Maquina = new HashSet<Maquina>();
        }
    
        public int idInstituicao { get; set; }
        [DisplayName("Raz�o Social")]
        public string razaoSocial { get; set; }
        [DisplayName("Nome Fantasia")]
        public string nomeFantasia { get; set; }
        [DisplayName("CNPJ")]
        public string cnpj { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Maquina> Maquina { get; set; }
    }
}
