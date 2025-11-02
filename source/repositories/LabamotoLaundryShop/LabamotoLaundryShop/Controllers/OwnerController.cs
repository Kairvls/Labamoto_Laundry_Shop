using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabamotoLaundryShop.Controllers
{
    public class OwnerController : Controller
    {
        // Owner Dashboard Page
        public ActionResult Dashboard()
        {
            // Check if logged in
            if (Session["OwnerUsername"] == null)
                return RedirectToAction("OwnerLogin", "Account");

            ViewBag.OwnerName = Session["OwnerUsername"];
            return View();
        }
    }
}