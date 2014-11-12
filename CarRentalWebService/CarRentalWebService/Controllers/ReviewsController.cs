using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRentalWebService.Models;

namespace CarRentalWebService.Controllers
{
    public class ReviewsController : Controller
    {
        private DbContextModel db = new DbContextModel();

        // tạo View Index hiển thị tất cả các reviews của khách hàng
        public ActionResult Index()
        {
            var reviews = db.Reviews.Include(r => r.Model);
            return View(reviews.ToList());
        }

        //tạo view Detail hiển thị 1 review của khách hàng
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // tạo view tạo 1 review 
        public ActionResult Create()
        {
            ViewBag.Model_Id = new SelectList(db.CarModels, "Id", "Name");
            return View();
        }

        // add 1 review vào database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Model_Id,Content,Stars,email")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Model_Id = new SelectList(db.CarModels, "Id", "Name", review.Model_Id);
            return View(review);
        }

        // tạo view sửa 1 review được chọn
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.Model_Id = new SelectList(db.CarModels, "Id", "Name", review.Model_Id);
            return View(review);
        }

        // hàm cập nhật 1 review đã chọn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Model_Id,Content,Stars,email")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Model_Id = new SelectList(db.CarModels, "Id", "Name", review.Model_Id);
            return View(review);
        }

        // tạo view xoá 1 review được chọn
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // hàm xoá 1 review được chọn và cập nhật lại database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
