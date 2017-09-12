﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartPrint;
using SmartPrint.CustomLibraries;
using SmartPrint.Models;

namespace SmartPrint.Controllers
{
    public class UsersController : Controller
    {
        private MainDbContext db = new MainDbContext();

       

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
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

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "UserType");

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FName,LName,UserEmail,UserPass,UserTypeId,UserCode,UserPhone,IsActive,AddedBy,AddedOn,EditedBy,EditedOn,RowStatus")] Users users)
        {
            if (ModelState.IsValid)
            {
                var encryptedPassword = CustomEnrypt.Encrypt(users.UserPass);
                users.UserPass = encryptedPassword;
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           // ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "UserType", users.UserTypeId);
            return View(users);
        }

        // GET: Users/Edit/5
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
            var decryptPassword = CustomDecrypt.Decrypt(users.UserPass);
            users.UserPass = decryptPassword;
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "UserType",users.UserTypeId);

            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FName,LName,UserEmail,UserPass,UserTypeId,UserCode,UserPhone,IsActive,EditedBy,EditedOn,RowStatus", Exclude = "AddedBy,AddedOn")] Users users)
        {
            if (ModelState.IsValid)
            {
                var encryptedPassword = CustomEnrypt.Encrypt(users.UserPass);
                users.UserPass = encryptedPassword;
               // db.Entry(users).Property(uco => uco.AddedBy).IsModified = false;
               // db.Entry(users).Property(uco => uco.AddedOn).IsModified = false;
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Delete/5
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

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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