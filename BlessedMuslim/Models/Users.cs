using System;
using System.Collections.Generic;

namespace BlessedMuslim.Models
{
    public partial class Users
    {
        public Users()
        {
            DsrApplicationForm = new HashSet<DsrApplicationForm>();
            Retailers = new HashSet<Retailers>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? RoleId { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? ReportTo { get; set; }
        public string HubId { get; set; }
        public string UserImage { get; set; }
        public string Document { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<DsrApplicationForm> DsrApplicationForm { get; set; }
        public virtual ICollection<Retailers> Retailers { get; set; }
    }
}
