﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartPrint;
using SmartPrint.Models;

namespace SmartPrint.Controllers
{
    public class UserTxnsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: UserTxns
        public ActionResult Index()
        {
            return View(db.UserTxns.ToList());
        }

        // GET: UserTxns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTxns userTxns = db.UserTxns.Find(id);
            if (userTxns == null)
            {
                return HttpNotFound();
            }
            return View(userTxns);
        }

        // GET: UserTxns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserTxns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TxnId,UserId,TxnType,TxnAmount,TxnDateTiime,TxnBalance,TxnRefJobId,TxnStatus,AddedBy,AddedOn,EditedBy,EditedOn,RowStatus")] UserTxns userTxns)
        {
            if (ModelState.IsValid)
            {
                db.UserTxns.Add(userTxns);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userTxns);
        }

        // GET: UserTxns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTxns userTxns = db.UserTxns.Find(id);
            if (userTxns == null)
            {
                return HttpNotFound();
            }
            return View(userTxns);
        }

        // POST: UserTxns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TxnId,UserId,TxnType,TxnAmount,TxnDateTiime,TxnBalance,TxnRefJobId,TxnStatus,AddedBy,AddedOn,EditedBy,EditedOn,RowStatus")] UserTxns userTxns)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTxns).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userTxns);
        }

        // GET: UserTxns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTxns userTxns = db.UserTxns.Find(id);
            if (userTxns == null)
            {
                return HttpNotFound();
            }
            return View(userTxns);
        }

        // POST: UserTxns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserTxns userTxns = db.UserTxns.Find(id);
            db.UserTxns.Remove(userTxns);
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
