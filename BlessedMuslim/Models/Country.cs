using System;
using System.Collections.Generic;

namespace BlessedMuslim.Models
{
    public partial class Country
    {
        public Country()
        {
            Areas = new HashSet<Areas>();
            Cities = new HashSet<Cities>();
            States = new HashSet<States>();
        }

        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Areas> Areas { get; set; }
        public virtual ICollection<Cities> Cities { get; set; }
        public virtual ICollection<States> States { get; set; }
    }
}
