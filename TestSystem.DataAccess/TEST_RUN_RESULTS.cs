//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestSystem.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class TEST_RUN_RESULTS
    {
        public int Id { get; set; }
        public int AlgorithmId { get; set; }
        public Nullable<int> TruePositiveNumber { get; set; }
        public Nullable<int> FalseNegativeNumber { get; set; }
        public Nullable<int> TrueNegativeNumber { get; set; }
        public Nullable<int> FalsePositiveNumber { get; set; }
        public int NumberOfRuns { get; set; }
        public string OtherResults { get; set; }
    
        public virtual ALGORITHMS ALGORITHMS { get; set; }
        public virtual TEST_RUNS TEST_RUNS { get; set; }
    }
}