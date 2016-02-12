using CryptoBank.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace CryptoBank {
    public class HelperTools {
        BitcoinRates b;

        public HelperTools() {
            WebClient client = new WebClient();
            string json = client.DownloadString("https://api.coindesk.com/v1/bpi/currentprice.json");
            b = JsonConvert.DeserializeObject<BitcoinRates>(json);
        }

        public float GetConversionRate() {
            return b.bpi.EUR.rate;
        }

        public string GetLastTimeUpdated() {
            return b.time.updatedUK;
        }
        
    }
}