//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VideoGamesEncyclopedia.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class rating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public float Rate { get; set; }
    
        public virtual aspnetuser aspnetuser { get; set; }
        public virtual product product { get; set; }
    }
}
