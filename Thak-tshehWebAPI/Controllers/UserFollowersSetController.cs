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
    public class UserFollowersSetController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserFollowersSet
        public ActionResult Index()
        {
            var userFollowers = db.UserFollowers.Include(u => u.User);
            return View(userFollowers.ToList());
        }

        // GET: UserFollowersSet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserFollowers userFollowers = db.UserFollowers.Find(id);
            if (userFollowers == null)
            {
                return HttpNotFound();
            }
            return View(userFollowers);
        }

        // GET: UserFollowersSet/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.User, "Id", "Account");
            return View();
        }

        // POST: UserFollowersSet/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,FollowingUserId,CreatDate")] UserFollowers userFollowers)
        {
            if (ModelState.IsValid)
            {
                db.UserFollowers.Add(userFollowers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.User, "Id", "Account", userFollowers.UserId);
            return View(userFollowers);
        }

        // GET: UserFollowersSet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserFollowers userFollowers = db.UserFollowers.Find(id);
            if (userFollowers == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.User, "Id", "Account", userFollowers.UserId);
            return View(userFollowers);
        }

        // POST: UserFollowersSet/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,FollowingUserId,CreatDate")] UserFollowers userFollowers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userFollowers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.User, "Id", "Account", userFollowers.UserId);
            return View(userFollowers);
        }

        // GET: UserFollowersSet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserFollowers userFollowers = db.UserFollowers.Find(id);
            if (userFollowers == null)
            {
                return HttpNotFound();
            }
            return View(userFollowers);
        }

        // POST: UserFollowersSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserFollowers userFollowers = db.UserFollowers.Find(id);
            db.UserFollowers.Remove(userFollowers);
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
