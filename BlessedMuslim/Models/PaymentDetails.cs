using System;
using System.Collections.Generic;

namespace BlessedMuslim.Models
{
    public partial class PaymentDetails
    {
        public long Id { get; set; }
        public string TransactionNumber { get; set; }
        public long? RetailerId { get; set; }
        public string Details { get; set; }
        public string PaymentMode { get; set; }
        public string Attachment { get; set; }
        public string PaymentStatus { get; set; }
        public decimal? AmountDue { get; set; }
        public decimal? AmountPaid { get; set; }
        public int? UserId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Retailers Retailer { get; set; }
        public virtual Users User { get; set; }
    }
}
