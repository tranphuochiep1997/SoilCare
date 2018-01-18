//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoilCareWebAPI.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Soil
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Soil()
        {
            this.Plants = new HashSet<Plant>();
        }
    
        public string Soil_id { get; set; }
        public string Soil_name { get; set; }
        public Nullable<double> Min_nutrient { get; set; }
        public Nullable<double> Max_nutrient { get; set; }
        public Nullable<double> Min_humidity { get; set; }
        public Nullable<double> Max_humidity { get; set; }
        public Nullable<double> Min_acidity { get; set; }
        public Nullable<double> Max_acidity { get; set; }
        public Nullable<double> Min_porosity { get; set; }
        public Nullable<double> Max_porosity { get; set; }
        public Nullable<double> Min_water_retention { get; set; }
        public Nullable<double> Max_water_retention { get; set; }
        public Nullable<double> Min_salinity { get; set; }
        public Nullable<double> Max_salinity { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plant> Plants { get; set; }
    }
}