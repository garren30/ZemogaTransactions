using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TransactionsWebApp.Models;

namespace TransactionsWebApp.Controllers
{
    [Authorize]
    [Authorize(Roles = "1")]
    public class UsersController : Controller
    {

        private TransactionsBDEntities db = new TransactionsBDEntities();

        /** show all users in the index page*/
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Roles);
            return View(users.ToList());
        }

        /** show the user information that match with the id sended */
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        /** Show the creation page, this is empty*/
        public ActionResult Create()
        {
            ViewBag.rol_id = new SelectList(db.Roles, "Id", "rol");
            return View();
        }

        
        
        [HttpPost]
        /** Save the data sender by the form inserting a new record on the bd*/
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,username,password,rol_id,name,created_at")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.password = EncodePassword(users.password);
                users.created_at = DateTime.Now;
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.rol_id = new SelectList(db.Roles, "Id", "rol", users.rol_id);
            return View(users);
        }

        /** This method take a string and return this string encrypted*/
        public static string EncodePassword(string originalPassword)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword);
            byte[] hash = sha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }

        /** Show the edit page, with the user information that match with the id sended */
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.rol_id = new SelectList(db.Roles, "Id", "rol", users.rol_id);
            return View(users);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        /** Save the data sender by the form updating this record on the bd*/
        public ActionResult Edit([Bind(Include = "Id,username,password,rol_id,name")] Users users)
        {
            if (ModelState.IsValid)
            {
                using (TransactionsBDEntities dc = new TransactionsBDEntities())
                {
                    Users us = (from p in dc.Users where p.Id == users.Id select p).FirstOrDefault();
                    us.username = users.username;
                    us.rol_id = users.rol_id;
                    us.name = users.name;
                    if (us.password != users.password && users.password != null)
                    {
                        us.password = EncodePassword(users.password);
                    }
                    dc.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.rol_id = new SelectList(db.Roles, "Id", "rol", users.rol_id);
            return View(users);
        }

        /** Show the delete page, with the user information that match with the id sended */
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        /** this method eliminates the record from the db*/
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
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
