using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlessedMuslim.Models
{
    public partial class RetailersView
    {
        public long Id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessCatName { get; set; }
        public string City { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostCode { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string ShopPhone { get; set; }
        public string MobileNumber { get; set; }
        public string RefCode { get; set; }
        public string CreatedDate { get; set; }
    }
}