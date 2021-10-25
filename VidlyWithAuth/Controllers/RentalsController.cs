using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VidlyWithAuth.Controllers
{
    public class RentalsController : Controller
    {
        // GET: Rentals
        public ActionResult Index()
        {
            return View("");
        }
        public ActionResult New()
        {
            return View("New");
        }

        public ActionResult Edit(int id)
        {
            return View("Edit");
        }

    }
}