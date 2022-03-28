using System;
using System.Collections.Generic;

namespace BlessedMuslim.Models
{
    public partial class Employee
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int? AreaId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public int? UserId { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string Photo { get; set; }
        public string Idphoto { get; set; }
        public string AccountNo { get; set; }
        public string SortCode { get; set; }
        public string RepId { get; set; }
        public long? ManagerId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual Areas Area { get; set; }
        public virtual Users User { get; set; }
    }
}
