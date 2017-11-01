using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using TransactionsApi.Models;

namespace TransactionsApi.Controllers
{
    /** In this class we have the method to validate a valid user*/
    public class UsersController : ApiController
    {
        private TransactionsBDEntities db = new TransactionsBDEntities();

        [HttpPost]
        /** This method validate the user info and if it match will return the iduser to be user in the apis headers calls we could return more fields like token
         or somethink like that*/
        public UserData ValidateUser([FromBody] UserData userd)
        {
            string pass = EncodePassword(userd.password);
            Users usr = (from p in db.Users where p.username == userd.username && p.password == pass select p).FirstOrDefault();
            if (usr != null)
            {
                return new UserData() {
                    Id = usr.Id,
                };
            }
            else
            {
                return null;
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
    }
}
