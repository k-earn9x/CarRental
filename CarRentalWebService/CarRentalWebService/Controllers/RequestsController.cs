using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRentalWebService.Models;
using System.Collections;

namespace CarRentalWebService.Controllers
{
    public class RequestsController : Controller
    {
        private DbContextModel db = new DbContextModel();

        private List<SelectListItem> states = new List<SelectListItem>
            {
                new SelectListItem {Value = "0", Text = "Pending"},
                new SelectListItem {Value = "1", Text = "Paid"}
            };

        //xuất danh sách các requests
        public ActionResult Index()
        {
            var requests = db.Requests.Include(r => r.City).Include(r => r.Model);
            return View(requests.ToList());
        }

        // hiển thị thông tin của request được chọn
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // hiển thị thông tin lên View Tạo một Requset
        public ActionResult Create()
        {
            ViewBag.State = new SelectList(states,"Value", "Text");
            ViewBag.City_Id = new SelectList(db.Cities, "Id", "Name");
            ViewBag.Model_Id = new SelectList(db.CarModels, "Id", "Name");
            return View();
        }

        // tạo một request và cập nhật vào database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Phone,FromDate,ToDate,PriceTotal,State,Model_Id,City_Id")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.State = new SelectList(states, request.State);
            ViewBag.City_Id = new SelectList(db.Cities, "Id", "Name", request.City_Id);
            ViewBag.Model_Id = new SelectList(db.CarModels, "Id", "Name", request.Model_Id);
            return View(request);
        }

        // hiển thị View Sửa 1 request
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }

            ViewBag.State = new SelectList(states, "Value", "Text");
            ViewBag.City_Id = new SelectList(db.Cities, "Id", "Name", request.City_Id);
            ViewBag.Model_Id = new SelectList(db.CarModels, "Id", "Name", request.Model_Id);
            return View(request);
        }

        // hàm edit cập nhật 1 request xuống database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Phone,FromDate,ToDate,PriceTotal,State,Model_Id,City_Id")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.State = new SelectList(states, "Value", "Text");
            ViewBag.City_Id = new SelectList(db.Cities, "Id", "Name", request.City_Id);
            ViewBag.Model_Id = new SelectList(db.CarModels, "Id", "Name", request.Model_Id);
            return View(request);
        }

        // hiển thị View Xoá 1 request
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // chức năng xoá 1 request đã chọn và cập nhật lại database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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
