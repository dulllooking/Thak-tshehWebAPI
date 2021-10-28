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
    public class ActivitiesSetController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivitiesSet
        public ActionResult Index()
        {
            return View(db.Activity.ToList());
        }

        // GET: ActivitiesSet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: ActivitiesSet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivitiesSet/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Image,ActivityClass,ActivityType,ActivityVenue,Software,Area,Address,AddressRemark,ActivityStartDate,ActivityEndDate,Summary,ContentText,PleaseNote,OrganizerName,OrganizerPhone,OrganizerMail,Link,Price,LimitNumber,StartAcceptDate,EndAcceptDate,PostState,FreeCost,ApplicantNumber,ApplicantFull,Views,CollectNumber,EvaluateStars,CreatDate")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activity.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activity);
        }

        // GET: ActivitiesSet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: ActivitiesSet/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Image,ActivityClass,ActivityType,ActivityVenue,Software,Area,Address,AddressRemark,ActivityStartDate,ActivityEndDate,Summary,ContentText,PleaseNote,OrganizerName,OrganizerPhone,OrganizerMail,Link,Price,LimitNumber,StartAcceptDate,EndAcceptDate,PostState,FreeCost,ApplicantNumber,ApplicantFull,Views,CollectNumber,EvaluateStars,CreatDate")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: ActivitiesSet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: ActivitiesSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activity.Find(id);
            db.Activity.Remove(activity);
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
