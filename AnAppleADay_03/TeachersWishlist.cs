//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnAppleADay_03
{
    using System;
    using System.Collections.Generic;
    
    public partial class TeachersWishlist
    {
        public int listId { get; set; }
        public string teachId { get; set; }
        public string itemName { get; set; }
        public string itemAbout { get; set; }
        public decimal cost { get; set; }
        public decimal current { get; set; }
        public System.DateTime expDate { get; set; }
    
        public virtual Teacher Teacher { get; set; }
    }
}
