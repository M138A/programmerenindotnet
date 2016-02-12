using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CryptoBank.Models {
    public class BitcoinRates {

        public Time time;
        public string disclaimer;
        public Bpi bpi;
    }

    public class Time {
        public string updated;
        public string updatedISO;
        public string updatedUK;

    }

    public class Bpi {
        public Currency USD;
        public Currency GBP;
        public Currency EUR;
    }

    public class Currency {
        public string code;
        public string symbol;
        public float rate;
        public string description;
        public float rate_float;

    }


}