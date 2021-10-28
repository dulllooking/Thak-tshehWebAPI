using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Thak_tshehWebAPI.Models;

namespace Thak_tshehWebAPI.Controllers
{
    public class ActivityLogsSetController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityLogsSet
        public ActionResult Index()
        {
            var activityLog = db.ActivityLog.Include(a => a.Activity).Include(a => a.User);
            return View(activityLog.ToList());
        }

        // GET: ActivityLogsSet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityLog activityLog = db.ActivityLog.Find(id);
            if (activityLog == null)
            {
                return HttpNotFound();
            }
            return View(activityLog);
        }

        // GET: ActivityLogsSet/Create
        public ActionResult Create()
        {
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name");
            ViewBag.UserId = new SelectList(db.User, "Id", "Account");
            return View();
        }

        // POST: ActivityLogsSet/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ActivityId,ActivityPrice,OrderState,UserId,UserName,UserMobilePhone,CreatDate")] ActivityLog activityLog)
        {
            if (ModelState.IsValid)
            {
                db.ActivityLog.Add(activityLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name", activityLog.ActivityId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Account", activityLog.UserId);
            return View(activityLog);
        }

        // GET: ActivityLogsSet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityLog activityLog = db.ActivityLog.Find(id);
            if (activityLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name", activityLog.ActivityId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Account", activityLog.UserId);
            return View(activityLog);
        }

        // POST: ActivityLogsSet/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ActivityId,ActivityPrice,OrderState,UserId,UserName,UserMobilePhone,CreatDate")] ActivityLog activityLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name", activityLog.ActivityId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Account", activityLog.UserId);
            return View(activityLog);
        }

        // GET: ActivityLogsSet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityLog activityLog = db.ActivityLog.Find(id);
            if (activityLog == null)
            {
                return HttpNotFound();
            }
            return View(activityLog);
        }

        // POST: ActivityLogsSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityLog activityLog = db.ActivityLog.Find(id);
            db.ActivityLog.Remove(activityLog);
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
