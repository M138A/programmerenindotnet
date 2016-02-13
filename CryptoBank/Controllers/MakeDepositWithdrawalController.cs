using CryptoBank.Cryptobankservice;
using CryptoBank.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace CryptoBank.Controllers
{
    public class MakeDepositWithdrawalController : HomeController {

        [HttpPost]
        public ActionResult ConfirmDepositWithDrawal(DepositWithdrawalInfo m) {
            CryptoBankServiceSoapClient x = new CryptoBankServiceSoapClient();
            x.MakeDepositWIthdrawal(loggedInUser.accountnumber, m.amount, m.deposit);
            return View("~/Views/Home/Index.cshtml");
        }

    }
}