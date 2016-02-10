using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CryptoBank.Models;
using System.Diagnostics;
using CryptoBank.Cryptobankservice;
using System.Text.RegularExpressions;

namespace CryptoBank.Controllers
{
    public class HomeController : Controller
    {
        static UserModel loggedInUser = new UserModel();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Transfer(int? msgid)
        {
            
            if(msgid == 1) ViewBag.Message = "Transfer Succeeded";
            else if (msgid == 0) ViewBag.Message = "Something went wrong";
            Debug.WriteLine(msgid);


            return View(new TransferModel());
        }
       
        public ActionResult ProcessTransfer(TransferModel tm)
        {
            CryptoBankServiceSoapClient x = new CryptoBankServiceSoapClient();

            string receiverAccountNumber = tm.receiverAccountNumber;
            int amountOfBitCoins = tm.receiverAmount;
            string senderAccountNumber = loggedInUser.accountnumber;


            Debug.WriteLine("amount ; " + amountOfBitCoins.ToString()); 
            Debug.WriteLine("receiver : " + receiverAccountNumber);
            Debug.WriteLine("sender : " + senderAccountNumber);

            senderAccountNumber = Regex.Replace(senderAccountNumber, @"\s+", "");
            receiverAccountNumber = Regex.Replace(receiverAccountNumber, @"\s+", "");
            if (x.TransferMoney(amountOfBitCoins, receiverAccountNumber, senderAccountNumber))
                return RedirectToAction("Transfer", "Home", new { msgid = 1 });
            else
                return RedirectToAction("Transfer", "Home", new { msgid = 0 });
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel user)
        {
            System.Diagnostics.Debug.WriteLine(user.email + " " + user.password);
            //check with database
            CryptoBankServiceSoapClient webService = new CryptoBankServiceSoapClient();
            if (webService.ValidateLogin(user.email, user.password))
            {
                loggedInUser = webService.GetUserByLogin(user.email, user.password);
                Session["LoggedUserAccountNumber"] = loggedInUser.accountnumber.ToString();
                Session["LoggedUserName"] = loggedInUser.name.ToString();
                return RedirectToAction("AfterLogin");
            }

            return View(user);
        }

        public ActionResult AfterLogin()
        {
            if (Session["LoggedUserAccountNumber"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            UserModel registerUSer = user;
            registerUSer.accountnumber = "NL01CRYPTO" + new Random().Next(000100000, 999999999);
            registerUSer.balance = 0;

            if (ModelState.IsValid)
            {
                CryptoBankServiceSoapClient webservice = new CryptoBankServiceSoapClient();
                webservice.RegisterUser(registerUSer);
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}