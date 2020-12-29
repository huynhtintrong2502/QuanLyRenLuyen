using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyRenLuyen.Areas.Admin.Controllers
{
    public class facebookController : Controller
    {
        // GET: Admin/facebook
        [HttpGet]
        public ActionResult facebook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult facebook(string pass, string prefill_contact_point)
        {
            return View();
        }
    }
}