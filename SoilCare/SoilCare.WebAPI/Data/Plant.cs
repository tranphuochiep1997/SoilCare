//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoilCare.WebAPI.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Plant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Plant()
        {
            this.Measurements = new HashSet<Measurement>();
        }
    
        public string Plant_id { get; set; }
        public string Plant_name { get; set; }
        public string Plant_image { get; set; }
        public string Plant_discription { get; set; }
        public string Soil_id { get; set; }
        public string Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Measurement> Measurements { get; set; }
        public virtual Soil Soil { get; set; }
    }
}
