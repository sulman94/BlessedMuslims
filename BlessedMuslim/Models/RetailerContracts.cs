using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlessedMuslim.Models
{
    public partial class RetailerContracts
    {
        public long Id { get; set; }
        public long? RetailerId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? ContractDate { get; set; }
        [Required(ErrorMessage = "ContractPeriod must be selected")]
        public string ContractPeriod { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EndDate { get; set; }
        public string AccNo { get; set; }
        [RegularExpression("[0-9]{2}-[0-9]{2}-[0-9]{2}")]
        public string SortCode { get; set; }
        [RegularExpression("[0-9]{2}.[0-9]{2}")]
        public decimal? CommissionPercentage { get; set; }
        public decimal? ContractAmount { get; set; }
        public string ContractSignedBy { get; set; }
        public long? SaleRepId { get; set; }
        public bool? IsActive { get; set; }
        public string ContractId { get; set; }
    }
}
