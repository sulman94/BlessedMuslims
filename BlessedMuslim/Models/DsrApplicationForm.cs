using System;
using System.Collections.Generic;

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
        public long? AreaId { get; set; }
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
    }
    public partial class DsrApplicationFormView
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dob { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }
        public string ContactNumber { get; set; }
        public string AreaName { get; set; }
        public string SubmitDate { get; set; }
    }
}
