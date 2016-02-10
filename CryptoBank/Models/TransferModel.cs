using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoBank.Models
{
    public class TransferModel
    {
        public int receiverAmount { get; set; }
        public string receivername { get; set; }
        public string receiverAccountNumber { get; set; }
    }
}