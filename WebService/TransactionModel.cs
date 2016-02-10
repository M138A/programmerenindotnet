using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoBank.Models
{
    public class TransactionModel
    {
        public int transactionnumber { get; set; }
        public string transactiontype { get; set; }
        public string date { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        public double amount { get; set; }
    }
}