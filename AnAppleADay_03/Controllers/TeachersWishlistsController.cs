using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AnAppleADay_03;
using AnAppleADay_03.Models;
using AnAppleADay_03.Models.ViewModels;

namespace AnAppleADay_03.Controllers
{
    public class TeachersWishlistsController : Controller
    {
        private AnAppleADay_01Entities db = new AnAppleADay_01Entities();

        // GET: TeachersWishlists
        public ActionResult Index()
        {
            var teachersWishlists = db.TeachersWishlists.Include(t => t.Teacher);
            return View(teachersWishlists.ToList());
        }

        public ActionResult Explore(int? id)
        {
            int maxWishlists = 10;

            if (id != null)
            {
                maxWishlists = id.Value;
            }

            var wishlists = db.TeachersWishlists
                .Where(w => w.expDate > DateTime.Now)
                .OrderBy(w => w.expDate)
                .Take(maxWishlists);

            return View(wishlists.ToList());

        }

        // GET: TeachersWishlists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeachersWishlist teachersWishlist = db.TeachersWishlists.Find(id);
            if (teachersWishlist == null)
            {
                return HttpNotFound();
            }
            
            var obj = new WishlistDonations(teachersWishlist);
            return View(obj);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(WishlistDonations donationModel)
        {
            if (ModelState.IsValid)
            {
                var blah = db.TeachersWishlists.Find(donationModel.listId);

                blah.current = blah.current + donationModel.donation;

                db.Entry(blah).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", new {id = blah.listId});
            }


                return View(donationModel);
        }

        // GET: TeachersWishlists/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.teachId = new SelectList(db.Teachers, "teachId", "firstName");
            return View();
        }

        // POST: TeachersWishlists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "listId,teachId,itemName,itemAbout,cost,current,expDate")] TeachersWishlist teachersWishlist)
        {
            if (ModelState.IsValid)
            {
                EmailUtility.sendMail("anappleaday14789@gmail.com", "Your List for item "+teachersWishlist.itemName+" was created. It will expire on "+teachersWishlist.expDate);
                db.TeachersWishlists.Add(teachersWishlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.teachId = new SelectList(db.Teachers, "teachId", "firstName", teachersWishlist.teachId);
            return View(teachersWishlist);
        }

        // GET: TeachersWishlists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeachersWishlist teachersWishlist = db.TeachersWishlists.Find(id);
            if (teachersWishlist == null)
            {
                return HttpNotFound();
            }
            ViewBag.teachId = new SelectList(db.Teachers, "teachId", "firstName", teachersWishlist.teachId);
            return View(teachersWishlist);
        }

        // POST: TeachersWishlists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "listId,teachId,itemName,itemAbout,cost,current,expDate")] TeachersWishlist teachersWishlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teachersWishlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.teachId = new SelectList(db.Teachers, "teachId", "firstName", teachersWishlist.teachId);
            return View(teachersWishlist);
        }

        // GET: TeachersWishlists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeachersWishlist teachersWishlist = db.TeachersWishlists.Find(id);
            if (teachersWishlist == null)
            {
                return HttpNotFound();
            }
            return View(teachersWishlist);
        }

        // POST: TeachersWishlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeachersWishlist teachersWishlist = db.TeachersWishlists.Find(id);
            db.TeachersWishlists.Remove(teachersWishlist);
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
