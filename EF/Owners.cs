//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Owners
    {
        public System.Guid kOwner { get; set; }
        public string Name { get; set; }
        public System.Guid kPet { get; set; }
    
        public virtual Pets Pets { get; set; }
    }
}
