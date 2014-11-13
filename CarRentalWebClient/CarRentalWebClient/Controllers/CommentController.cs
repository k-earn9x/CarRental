using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRentalWebClient.Controllers
{
    public class CommentController : SiteController
    {
        // GET: Commnent
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection f)
        {
            string email = f["email"];
            try
            {
                var request = Db.Requests.Where(p => p.Email.Equals(email)).ToList();
                bool ktra = request.Any();
                if (ktra)
                {
                    foreach (CarRentalServiceReference.Request a in request)
                    {
                        Session["email"] = a.Email;
                    }
                    return RedirectToAction("Review", "Comment");
                }
                else
                {
                    return RedirectToAction("Index", "Comment");
                }
               
            }
            catch
            {
                return RedirectToAction("Index", "Comment");
            }
            
        }
        public ActionResult Review()
        {
            if (Session["email"] != null)
            {
                //var req = Db.Requests.Where(p => p.Email.Equals(Session["email"])).ToList();
                //ViewBag.req = req;
                var model = Db.CarModels.ToList();
                var returnModels = new List<CarRentalServiceReference.CarModel>();
                foreach (var a in model)
                {
                    var request = Db.Requests.Where(p => p.Email.Equals(Session["email"]) && a.Id == p.Model_Id).Count();
                    if(request>0)
                    {
                        returnModels.Add(a);
                    }
                }

                ViewBag.Model_Id = new SelectList(returnModels, "Id", "Name");
               
                //ViewBag.req = new SelectList(request, "Model_Id", "NameModel");
              
                return View();
                
            }
            return View();
        }
        [HttpPost]
        public ActionResult Review(FormCollection f)
        {
            if (Session["email"] != null)
            {
                int model_id = int.Parse(f["Model_Id"]);
                int star = int.Parse(f["star"]);
                string comment = f["comment"];
                CarRentalServiceReference.Review review = new CarRentalServiceReference.Review();
                review.Model_Id = model_id;
                review.Stars = star;
                review.Content = comment;
                review.email = Session["email"].ToString();
                Db.AddToReviews(review);
                Db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Comment");
            }
           
           
        }
    }
}