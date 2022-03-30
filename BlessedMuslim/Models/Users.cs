using System;
using System.Collections.Generic;

namespace BlessedMuslim.Models
{
    public partial class Users
    {
        public Users()
        {
            Employee = new HashSet<Employee>();
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

        public virtual Role Role { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Retailers> Retailers { get; set; }
    }
}
