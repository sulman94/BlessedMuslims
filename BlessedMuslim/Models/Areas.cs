using System;
using System.Collections.Generic;

namespace BlessedMuslim.Models
{
    public partial class Areas
    {
        public Areas()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public bool? IsActive { get; set; }

        public virtual Cities City { get; set; }
        public virtual Country Country { get; set; }
        public virtual States State { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
