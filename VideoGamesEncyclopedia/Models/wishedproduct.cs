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
    
    public partial class wishedproduct
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    
        public virtual product product { get; set; }
        public virtual user user { get; set; }
    }
}
