//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PointsOfInterest
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Fk_Hotel_Id { get; set; }
        public Nullable<int> Fk_Place_Id { get; set; }
        public Nullable<int> Fk_Museum_Id { get; set; }
        public string UserEmail { get; set; }
    
        public virtual Hotel Hotel { get; set; }
        public virtual Museum Museum { get; set; }
        public virtual Place Place { get; set; }
    }
}
