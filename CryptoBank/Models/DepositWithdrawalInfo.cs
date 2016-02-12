using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoBank.Models {
    public class DepositWithdrawalInfo {
        public string IBAN { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public bool deposit { get; set; }
    }
}