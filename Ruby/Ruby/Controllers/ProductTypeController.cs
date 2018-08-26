using Ruby.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ruby.Controllers
{
    public class ProductTypeController : Controller
    {

        public ActionResult Index(string id)
        {
            var context = new ApplicationDbContext();
            var types = context.ProductType.ToList();
            return View(types);
        }
    }
}