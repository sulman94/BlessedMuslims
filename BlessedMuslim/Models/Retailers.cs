using System;
using System.Collections.Generic;

namespace BlessedMuslim.Models
{
    public partial class Retailers
    {
        public long Id { get; set; }
        public string BusinessName { get; set; }
        public int? BusinessCategoryId { get; set; }
        public int? CityCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string ShopPhone { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? RegDate { get; set; }
        public int? RegBy { get; set; }
        public string Comments { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool TermsAgreement { get; set; }
        public string ReferenceCode { get; set; }

        public virtual Cities CityCodeNavigation { get; set; }
        public virtual Users RegByNavigation { get; set; }
    }
}
