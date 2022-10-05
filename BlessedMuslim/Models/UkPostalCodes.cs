using System;
using System.Collections.Generic;

namespace BlessedMuslim.Models
{
    public partial class UkPostalCodes
    {
        public long Id { get; set; }
        public string PostCode { get; set; }
        public string AreaCode { get; set; }
        public string District { get; set; }
        public string AreaName { get; set; }
        public string Region { get; set; }
        public string County { get; set; }
        public bool? IsActive { get; set; }
    }
}
