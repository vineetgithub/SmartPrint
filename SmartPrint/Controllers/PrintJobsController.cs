﻿using SmartPrint.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Management;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using SmartPrint.CustomLibaries;

namespace SmartPrint.Controllers
{
    public class PrintJobsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: PrintJobs
        public ActionResult Index()
        {
            return View(db.PrintJobs.ToList());
        }

        // GET: PrintJobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintJobs printJobs = db.PrintJobs.Find(id);
            if (printJobs == null)
            {
                return HttpNotFound();
            }
            return View(printJobs);
        }

        // GET: PrintJobs/Create
        public ActionResult Create(int? id)
        {

            PrintJobs model = new PrintJobs();

            UserDocs userDocsToPrint = db.UserDocs.Find(id);

            //ViewBag.UserDocsToPrint = userDocsToPrint;
            //model.JobId = userDocsToPrint
            model.UserId = int.Parse(User.Identity.GetUserId());
            model.DocId = userDocsToPrint.DocId;
            model.DocName = userDocsToPrint.DocName;
            model.DocTypeId = userDocsToPrint.DocTypeId;
            model.DocExt = userDocsToPrint.DocExt;
            model.DocFileNameOnServer =userDocsToPrint.DocFileName;
            model.DocFilePath = userDocsToPrint.DocFilePath;
            /*model.DocTotalPages = "";
            model.PrintCostId = "";
            model.MonoPages = "";
            model.ColorPages = "";
            model.IsColor = "";
            model.IsDuplex = "";
            model.IsCollate = "";
            model.UnitCost = "";
            model.MonoUnitCost = "";
            model.ColorUnitCost = "";
            model.UnitItem = "";
            model.JobRemarks = "";
            model.PagesFrom = "";
            model.PagesTo = "";
            model.NumCopies = "";
            model.TotalPageCount = "";
            model.TotalPageCost = "";
            model.CreditUsed = "";
            model.JobError = "";
            model.JobErrorRemarks = "";
            model.PrinterName = "";
            model.PrinterPath = "";
            model.JobStatusId = "";
            model.AddedBy = "";
            model.AddedOn = "";
            model.EditedBy = "";
            model.EditedOn = "";
            model.StatusId = "";
            */


            ViewBag.PrinterName = new SelectList(Printer.GetPrinterList(),"Value","Text");
            
            ViewBag.StatusId = new SelectList(db.RStatus, "StatusId", "StatusName");

            ViewBag.PrintCostId= new SelectList(db.PrintCosts, "PrintCostId", "Name");

            // ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "UserType");
            ViewBag.StatusId = new SelectList(db.RStatus, "StatusId", "StatusName");
           // ViewBag.UStatusId = new SelectList(db.UStatus, "UStatusId", "UStatusName");
            return View(model);
        }

        // POST: PrintJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobId,UserId,DocId,DocName,DocTypeId,DocExt,DocFileNameOnServer,DocFilePath,DocTotalPages,PrintcostId,MonoPages,ColorPages,IsColor,IsDuplex,IsCollate,UnitCost,MonoUnitcost,ColorUnitcost,UnitItem,JobRemarks,PagesFrom,PagesTo,NumCopies,TotalPageCount,TotalPageCost,CreditUsed,JobError,JobErrorRemarks,PrinterName,PrinterPath,JobStatusId,AddedBy,AddedOn,EditedBy,EditedOn,StatusId")] PrintJobs printJobs)
        {
            if (ModelState.IsValid)
            {
                db.PrintJobs.Add(printJobs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(printJobs);
        }

        // GET: PrintJobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintJobs printJobs = db.PrintJobs.Find(id);
            if (printJobs == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "UserType", users.UserTypeId);
            ViewBag.StatusId = new SelectList(db.RStatus, "StatusId", "StatusName", printJobs.StatusId);
            //ViewBag.UStatusId = new SelectList(db.UStatus, "UStatusId", "UStatusName", users.UStatusId);
            return View(printJobs);
        }

        // POST: PrintJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,UserId,DocId,DocName,DocTypeId,DocExt,DocFileNameOnServer,DocFilePath,DocTotalPages,PrintcostId,MonoPages,ColorPages,IsColor,IsDuplex,IsCollate,UnitCost,MonoUnitcost,ColorUnitcost,UnitItem,JobRemarks,PagesFrom,PagesTo,NumCopies,TotalPageCount,TotalPageCost,CreditUsed,JobError,JobErrorRemarks,PrinterName,PrinterPath,JobStatusId,EditedBy,EditedOn,StatusId",Exclude = "AddedBy,AddedOn")] PrintJobs printJobs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(printJobs).State = EntityState.Modified;
                db.Entry(printJobs).Property(uco => uco.AddedBy).IsModified = false;
                db.Entry(printJobs).Property(uco => uco.AddedOn).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(printJobs);
        }

        // GET: PrintJobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintJobs printJobs = db.PrintJobs.Find(id);
            if (printJobs == null)
            {
                return HttpNotFound();
            }
            return View(printJobs);
        }

        // POST: PrintJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                PrintJobs printJobs = db.PrintJobs.Find(id);
                printJobs.StatusId = 0; // on delete setting up the row status column to 0 for softdelete. 1 is active
                db.Entry(printJobs).State = EntityState.Modified;
                //db.Users.Remove(users);
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }

           
        }

        public ActionResult GetPrintCosts(int PrintCostId)
        {
            var printCost = db.PrintCosts.Where(c => c.PrintCostId== PrintCostId);
            
            return Json(printCost, JsonRequestBehavior.AllowGet);
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
