using FindIt.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestPlugin.Models;

namespace TestPlugin.Controllers
{
    public class TestPluginController:Controller
    {
        private readonly IStorage _storage;

        public TestPluginController(IStorage storage)
        {
            _storage = storage;
        }
        public ActionResult Configure()
        {
            ViewBag.Script = Scripts.testplugin;
            var dd = _storage;
            return PartialView(new ConfigureModel { MyProperty = "Cock Sucker" });
        }
    }
}
