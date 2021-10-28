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
    public class ActivityOpinionsSetController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityOpinionsSet
        public ActionResult Index()
        {
            var activityOpinion = db.ActivityOpinion.Include(a => a.Activity).Include(a => a.User);
            return View(activityOpinion.ToList());
        }

        // GET: ActivityOpinionsSet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityOpinion activityOpinion = db.ActivityOpinion.Find(id);
            if (activityOpinion == null)
            {
                return HttpNotFound();
            }
            return View(activityOpinion);
        }

        // GET: ActivityOpinionsSet/Create
        public ActionResult Create()
        {
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name");
            ViewBag.UserId = new SelectList(db.User, "Id", "Account");
            return View();
        }

        // POST: ActivityOpinionsSet/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ActivityId,Star,Opinion,CreatDate")] ActivityOpinion activityOpinion)
        {
            if (ModelState.IsValid)
            {
                db.ActivityOpinion.Add(activityOpinion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name", activityOpinion.ActivityId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Account", activityOpinion.UserId);
            return View(activityOpinion);
        }

        // GET: ActivityOpinionsSet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityOpinion activityOpinion = db.ActivityOpinion.Find(id);
            if (activityOpinion == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name", activityOpinion.ActivityId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Account", activityOpinion.UserId);
            return View(activityOpinion);
        }

        // POST: ActivityOpinionsSet/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ActivityId,Star,Opinion,CreatDate")] ActivityOpinion activityOpinion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityOpinion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityId = new SelectList(db.Activity, "Id", "Name", activityOpinion.ActivityId);
            ViewBag.UserId = new SelectList(db.User, "Id", "Account", activityOpinion.UserId);
            return View(activityOpinion);
        }

        // GET: ActivityOpinionsSet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityOpinion activityOpinion = db.ActivityOpinion.Find(id);
            if (activityOpinion == null)
            {
                return HttpNotFound();
            }
            return View(activityOpinion);
        }

        // POST: ActivityOpinionsSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityOpinion activityOpinion = db.ActivityOpinion.Find(id);
            db.ActivityOpinion.Remove(activityOpinion);
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
