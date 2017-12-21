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
    
    public partial class Measurement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Measurement()
        {
            this.SolutionOffers = new HashSet<SolutionOffer>();
        }
    
        public string Measure_id { get; set; }
        public Nullable<System.DateTime> Created_at { get; set; }
        public string Land_id { get; set; }
        public string Plant_id { get; set; }
        public Nullable<double> Nutrient { get; set; }
        public Nullable<double> Humidity { get; set; }
        public Nullable<double> Acidity { get; set; }
        public Nullable<double> Porosity { get; set; }
        public Nullable<double> Water_retention { get; set; }
        public Nullable<double> Salinity { get; set; }
        public Nullable<int> Rate { get; set; }
    
        public virtual Land Land { get; set; }
        public virtual Plant Plant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolutionOffer> SolutionOffers { get; set; }
    }
}
