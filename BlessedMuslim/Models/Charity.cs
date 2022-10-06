using System;
using System.Collections.Generic;

namespace BlessedMuslim.Models
{
    public partial class Charity
    {
        public long Id { get; set; }
        public string CharityId { get; set; }
        public string CharityNumber { get; set; }
        public string BusinessName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string Landline { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string SortCode { get; set; }
        public bool? IsActive { get; set; }
    }
}
