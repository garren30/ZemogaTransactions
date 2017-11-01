using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TransactionsWebApp.Models;

namespace TransactionsWebApp.Controllers
{
    /** In this class we implemented methos for login, logout and first page to show*/
    public class HomeController : Controller
    {
        [Authorize(Roles="1, 2, 3, 4")]
        /** this method  redirect to the view with the menu and user name*/
        public ActionResult Index()
        {
            return View();
        }


        /** this methos redirect to the login page, to allow the users to write their users and passwords*/
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        /** This method validate the user info and if it match will add the user info to the formsauthentications and redirect to the index, otherwise will return to 
         * login page and wil show the error */
        public ActionResult Login(Users userdata, string returnUrl)
        {
            TransactionsBDEntities db = new TransactionsBDEntities();
            string pass = EncodePassword(userdata.password);
            Users user = (from p in db.Users where p.username == userdata.username && p.password == pass select p).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.username, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid data");
                return View();
            }
        }


        /** This method take a string and return this string encrypted*/
        public static string EncodePassword(string originalPassword)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }


        [Authorize]
        /** this method remove the user information from the FormsAuthentication and redirect to login page*/
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
    }
}