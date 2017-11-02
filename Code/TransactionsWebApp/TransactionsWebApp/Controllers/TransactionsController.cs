using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TransactionsWebApp.Models;
using System.Web.Security;

namespace TransactionsWebApp.Controllers
{
    [Authorize]
    /** In this class we have all methods that use to work with transactions, just the rigth roles have acces to the different methods */
    public class TransactionsController : Controller
    {
        private TransactionsBDEntities db = new TransactionsBDEntities();

        // GET: Transactions
        [Authorize(Roles = "1, 2")]
        /** This method shows all transactions that we have in the db, this information is shown in the index page*/
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Clients).Include(t => t.Clients1).Include(t => t.Transactiontypes).Include(t => t.Users).Include(t => t.Users1);
            return View(transactions.ToList());
        }

        /** this method get one transactions that match with de code sended by url, this will display the information on detail page */
        [Authorize(Roles = "1, 3, 4")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        
        [Authorize(Roles = "1, 4")]
        /** Show the creation page, this is empty*/
        public ActionResult CreateTransaction()
        {
            ViewBag.transactiontype_id = new SelectList(db.Transactiontypes, "Id", "transactiontype");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1, 4")]
        /** This method will create a new transaction in the db with the information that is recived and using a stored procedure, after create a new record will return 
         this record on the details page*/
        public ActionResult CreateTransaction(FormCollection form)
        {
            try
            {
                //ViewBag.transactiontype_id = new SelectList(db.Transactiontypes, "Id", "transactiontype");
                //return View();
                ObjectParameter Identity = new ObjectParameter("Identity", typeof(String));
                db.GetTransactionData(form["txtclientorg"], form["txtclientdest"], float.Parse(form["amount"],System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(form["transactiontype_id"]), 1, Identity);
                return RedirectToAction("Details", new { id = Identity.Value });
            }
            catch (Exception exp)
            {
                return RedirectToAction("CreateTransaction");
            }
        }


        [Authorize(Roles = "1, 3")]
        /** this method will change the isfraud value to 1 to the transaction that match with the code sended to the method, also update the date and user who mark it 
          as fraud, this method wil return this record on the details page*/
        public ActionResult ReportFraud(int? id)
        {
            Transactions transaction = (from p in db.Transactions where p.Id == id select p).FirstOrDefault();
            transaction.isfraud = 1;
            transaction.usermarkfraud_id = 1;
            transaction.frauddate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = transaction.Id });
        }


        [Authorize(Roles = "1, 3")]
        /** Show the serach report fraud page, this is empty*/
        public ActionResult SearchtoReportFraud()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "1, 3")]
        /** validates id there are any record that match with the id and show the information on the details page, with a button to report transaction as fraud, 
         * if it has no match will return a message error*/
        public ActionResult SearchtoReportFraud(FormCollection form)
        {
            int code = 0;
            int.TryParse(form["TransactionCode"], out code);
            if (code != 0)
            {
                Transactions t = (from p in db.Transactions where p.Id == code select p).FirstOrDefault();
                if (t != null)
                {
                    return RedirectToAction("Details", new { id = code });
                }
                else
                {
                    ViewBag.Message = "Invalid Code";
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Invalid Code";
                return View();
            }
        }

        [Authorize(Roles = "1, 2")]
        /** Show the report page, this is empty*/
        public ActionResult Report()
        {
            IQueryable<Transactions> transactions = null;
            DateTime date = DateTime.Now;
            ViewBag.datei = date.Year+"-"+date.Month+"-"+date.Day;
            ViewBag.datef = date.Year + "-" + date.Month + "-" + date.Day;
            return View(transactions);
        }
        [HttpPost]
        [Authorize(Roles = "1, 2")]
        /** this method get the total of transactions that we have in the db, if the filters are emptys this will bring all information otherwise the information
         will be filtered by each filter that is sended in the url request, will Display the records information on the report page */
        public ActionResult Report(FormCollection form)
        {
            try
            {
                IQueryable<Transactions> transactions = from p in db.Transactions select p;

                if (form["initdate"] != "")
                {
                    DateTime datei = DateTime.Now;
                    DateTime.TryParse(form["initdate"], out datei);
                    transactions = transactions.Where(p => (p.TransactionDate >= datei));
                }
                if (form["finaldate"] != "")
                {
                    DateTime datef = DateTime.Now;
                    DateTime.TryParse(form["finaldate"], out datef);
                    transactions = transactions.Where(p => p.TransactionDate <= datef);
                }
                if (form["CodeClient"] != "")
                {
                    string client = form["CodeClient"];
                    transactions = transactions.Where(p => p.Clients1.ClientCode.Contains(client));
                }
                if (form["fraud"] == "1")
                {
                    transactions = transactions.Where(p => p.isfraud == 1);
                }
                ViewBag.datei = form["initdate"];
                ViewBag.datef = form["finaldate"];
                ViewBag.codec = form["CodeClient"];
                ViewBag.fraudcheck = form["fraud"];
                return View(transactions);
            }
            catch (Exception exp)
            {
                return RedirectToAction("Report");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
