using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindIt.Web.Controllers
{
    public class CraigsListController : Controller
    {
        //
        // GET: /CraigsList/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PublicInfo()
        {
            return PartialView();
        }
        public ActionResult Configure()
        {
            return PartialView();
        }
        public ActionResult List()
        {
            return PartialView();
        }
        public ActionResult Detail()
        {
            return PartialView();
        }

    }
}
