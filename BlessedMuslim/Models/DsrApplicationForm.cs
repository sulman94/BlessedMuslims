using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlessedMuslim.Models
{
    public partial class DsrApplicationForm
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }
        public string ContactNumber { get; set; }
        public int? AreaId { get; set; }
        public bool TermsAgreement { get; set; }
        public DateTime? SubmitDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public long? ApprovedBy { get; set; }
        public DateTime? RejectedDate { get; set; }
        public DateTime? RejectedBy { get; set; }
        public string Remarks { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsUserCreated { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"^[A-Za-z0-9]+@([a-zA-Z]+\\.)+[a-zA-Z]{2,6}]&")]
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string Photo { get; set; }
        public string Idphoto { get; set; }
        public string AccountNo { get; set; }
        public string SortCode { get; set; }
        public string RepId { get; set; }
        public bool? IsActive { get; set; }
        public string ReferenceCode { get; set; }
        public int? UserId { get; set; }

        public virtual Areas Area { get; set; }
        public virtual Users User { get; set; }
    }
}
