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
    
    public partial class SolutionOffer
    {
        public string Measure_id { get; set; }
        public string Solution_id { get; set; }
        public Nullable<double> Value_config { get; set; }
        public string Unit_symbol_config { get; set; }
        public string Unit_name_config { get; set; }
        public string Status { get; set; }
    
        public virtual Measure Measure { get; set; }
        public virtual Solution Solution { get; set; }
    }
}
