using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlessedMuslim.Models
{
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