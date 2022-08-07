using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlessedMuslim.Models
{
    public partial class DsrApplicationFormView
    {
        
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Dob { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string AreaName { get; set; }
        public string SubmitDate { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? RejectedDate { get; set; }
        public string AccountNo { get; set; }
        public string SortCode { get; set; }
        public string ReferenceCode { get; set; }
        public string Remarks { get; set; }
        public string Photo { get; set; }
        public string IdPhoto { get; set; }
    }
}