using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace BlessedMuslim.Models
{
    public class ContractPaymentViewModel
    {
        public long? SaleRepId { get; set; }
        public long? RetailerId { get; set; }
        public string ContractName { get; set; }
        public string RetailerName { get; set; }
        public string ContractPeriod { get; set; }
        public decimal ContractAmount { get; set; }
        public string RefNumber { get; set; }
        [Remote("IsTrxDateValid", "Validation", HttpMethod = "POST", ErrorMessage = "Please provide a valid date of birth.")]
        public DateTime TransactionDate { get; set; }
        public string Comments { get; set; }
    }
}

