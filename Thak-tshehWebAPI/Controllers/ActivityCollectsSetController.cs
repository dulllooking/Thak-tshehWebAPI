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
    public class ActivityCollectsSetController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityCollectsSet
        public ActionResult Index()
        {
            var activityCollect = db.ActivityCollect.Include(a => a.Activity).Include(a => a.User);
            return View(activityCollect.ToList());
        }

        // GET: ActivityCollectsSet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityCollect activityCollect = db.ActivityCollect.Find(id);
            if (activityCollect == null)
            {
                return HttpNotFound();
            }
            return View(activityCollect);
        }

        // GET: ActivityCollectsSet/Create
        public ActionResult Create()
        {
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name");
            ViewBag.UserId = new SelectList(db.User, "Id", "Account");
            return View();
        }

        // POST: ActivityCollectsSet/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ActivityId,CreatDate")] ActivityCollect activityCollect)
        {
            if (ModelState.IsValid)
            {
                db.ActivityCollect.Add(activityCollect);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name", activityCollect.ActivityId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Account", activityCollect.UserId);
            return View(activityCollect);
        }

        // GET: ActivityCollectsSet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityCollect activityCollect = db.ActivityCollect.Find(id);
            if (activityCollect == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name", activityCollect.ActivityId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Account", activityCollect.UserId);
            return View(activityCollect);
        }

        // POST: ActivityCollectsSet/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ActivityId,CreatDate")] ActivityCollect activityCollect)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityCollect).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name", activityCollect.ActivityId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Account", activityCollect.UserId);
            return View(activityCollect);
        }

        // GET: ActivityCollectsSet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityCollect activityCollect = db.ActivityCollect.Find(id);
            if (activityCollect == null)
            {
                return HttpNotFound();
            }
            return View(activityCollect);
        }

        // POST: ActivityCollectsSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityCollect activityCollect = db.ActivityCollect.Find(id);
            db.ActivityCollect.Remove(activityCollect);
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
