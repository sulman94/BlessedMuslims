using System;
using System.Collections.Generic;

namespace BlessedMuslim.Models
{
    public partial class Cities
    {
        public Cities()
        {
            Areas = new HashSet<Areas>();
            Retailers = new HashSet<Retailers>();
        }

        public int Id { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string CityName { get; set; }
        public bool? IsActive { get; set; }

        public virtual Country Country { get; set; }
        public virtual States State { get; set; }
        public virtual ICollection<Areas> Areas { get; set; }
        public virtual ICollection<Retailers> Retailers { get; set; }
    }
}
