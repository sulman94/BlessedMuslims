using System;
using System.Collections.Generic;

namespace BlessedMuslim.Models
{
    public partial class MasterAddresses
    {
        public int Id { get; set; }
        public string AddressName { get; set; }
        public bool? IsActive { get; set; }
    }
}
