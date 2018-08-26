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
    public class ProductController : Controller
    {

        public async Task<ActionResult> Index(string name)
        {
            var context = new ApplicationDbContext();
            //var type = await context.ProductType.FirstOrDefaultAsync(x=>x.Name==name);
            var products = await context.Product.Where(x=>x.Name==name).ToListAsync();
            
            return await Task.FromResult(View(products));
        }
    }
}