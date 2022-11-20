using System;
using System.Collections.Generic;

namespace BlessedMuslim.Models
{
    public partial class ContractPayments
    {
        public long Id { get; set; }
        public long? SaleRepId { get; set; }
        public long? RetailerId { get; set; }
        public string ContractId { get; set; }
        public string RefNumber { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Comments { get; set; }
        public bool? IsActive { get; set; }
        public long? UserId { get; set; }
    }
}
