using System;
using System.Collections.Generic;

namespace BlessedMuslim.Models
{
    public partial class BusinessCategories
    {
        public int Id { get; set; }
        public string Sdesc { get; set; }
        public string Ldesc { get; set; }
        public bool? IsActive { get; set; }
    }
}
