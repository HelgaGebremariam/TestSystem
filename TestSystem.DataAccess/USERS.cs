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
    
    public partial class USERS
    {
        public USERS()
        {
            this.ALGORITHMS = new HashSet<ALGORITHMS>();
            this.TEST_DATA_SETS = new HashSet<TEST_DATA_SETS>();
            this.TEST_RUNS = new HashSet<TEST_RUNS>();
            this.USER_SAVED_SETTINGS = new HashSet<USER_SAVED_SETTINGS>();
        }
    
        public int Id { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public string UserName { get; set; }
    
        public virtual ICollection<ALGORITHMS> ALGORITHMS { get; set; }
        public virtual ICollection<TEST_DATA_SETS> TEST_DATA_SETS { get; set; }
        public virtual ICollection<TEST_RUNS> TEST_RUNS { get; set; }
        public virtual USER_DETAILS USER_DETAILS { get; set; }
        public virtual USER_LOGIN_ROLES USER_LOGIN_ROLES { get; set; }
        public virtual ICollection<USER_SAVED_SETTINGS> USER_SAVED_SETTINGS { get; set; }
    }
}