using FindIt.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace TestPlugin.Controllers
{
    public class TestPluginController:Controller
    {
        private readonly IWorkContext _workContext;
        public TestPluginController(IWorkContext workContext)
        {
            _workContext = workContext;
        }
        public ActionResult Index()
        {
           
            return View();
        }
    }
}
