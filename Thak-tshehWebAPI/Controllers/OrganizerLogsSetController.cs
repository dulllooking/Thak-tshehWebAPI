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
    public class OrganizerLogsSetController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrganizerLogsSet
        public ActionResult Index()
        {
            var organizerLog = db.OrganizerLog.Include(o => o.Activity).Include(o => o.User);
            return View(organizerLog.ToList());
        }

        // GET: OrganizerLogsSet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizerLog organizerLog = db.OrganizerLog.Find(id);
            if (organizerLog == null)
            {
                return HttpNotFound();
            }
            return View(organizerLog);
        }

        // GET: OrganizerLogsSet/Create
        public ActionResult Create()
        {
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name");
            ViewBag.UserId = new SelectList(db.User, "Id", "Account");
            return View();
        }

        // POST: OrganizerLogsSet/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ActivityId,CreatDate")] OrganizerLog organizerLog)
        {
            if (ModelState.IsValid)
            {
                db.OrganizerLog.Add(organizerLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name", organizerLog.ActivityId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Account", organizerLog.UserId);
            return View(organizerLog);
        }

        // GET: OrganizerLogsSet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizerLog organizerLog = db.OrganizerLog.Find(id);
            if (organizerLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name", organizerLog.ActivityId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Account", organizerLog.UserId);
            return View(organizerLog);
        }

        // POST: OrganizerLogsSet/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ActivityId,CreatDate")] OrganizerLog organizerLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizerLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name", organizerLog.ActivityId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Account", organizerLog.UserId);
            return View(organizerLog);
        }

        // GET: OrganizerLogsSet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizerLog organizerLog = db.OrganizerLog.Find(id);
            if (organizerLog == null)
            {
                return HttpNotFound();
            }
            return View(organizerLog);
        }

        // POST: OrganizerLogsSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrganizerLog organizerLog = db.OrganizerLog.Find(id);
            db.OrganizerLog.Remove(organizerLog);
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
