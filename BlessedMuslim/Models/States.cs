using System;
using System.Collections.Generic;

namespace BlessedMuslim.Models
{
    public partial class States
    {
        public States()
        {
            Areas = new HashSet<Areas>();
            Cities = new HashSet<Cities>();
        }

        public int Id { get; set; }
        public int? CountryId { get; set; }
        public string StateName { get; set; }
        public bool? IsActive { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Areas> Areas { get; set; }
        public virtual ICollection<Cities> Cities { get; set; }
    }
}
