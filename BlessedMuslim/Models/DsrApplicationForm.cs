using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlessedMuslim.Models
{
    public partial class DsrApplicationForm : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (RejectedDate < SubmitDate)
            {
                yield return new ValidationResult(
                    errorMessage: "RejectedDate must be greater than SubmitDate",
                    memberNames: new[] { "RejectedDate" }
               );
            }

            if (ApprovedDate < SubmitDate)
            {
                yield return new ValidationResult(
                    errorMessage: "ApprovedDate must be greater than SubmitDate",
                    memberNames: new[] { "ApprovedDate" }
               );
            }
        }
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }
        public string ContactNumber { get; set; }
        public int? AreaId { get; set; }
        public bool TermsAgreement { get; set; }
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? SubmitDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ApprovedDate { get; set; }
        public long? ApprovedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? RejectedDate { get; set; }
        public long? RejectedBy { get; set; }
        public string Remarks { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public bool? IsUserCreated { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"^[A-Za-z0-9]+@([a-zA-Z]+\\.)+[a-zA-Z]{2,6}]&")]
        public string Email { get; set; }
        //[Compare("Email")]
        //public string ConfirmEmail { get; set; }
        public string Gender { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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
