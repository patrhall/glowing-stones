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
            var productTypes = new List<ProductType>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                productTypes = await context.Product.Where(x => x.Name == name)
                    .GroupBy(_ => _.Name)
                    .Select(_ => new ProductType { Name = _.Key, Products = _.Select(p=>p) }).ToListAsync(); ;
            }
            else
            {
                productTypes = await context.Product.GroupBy(_=>_.Name)
                    .Select(_=>new ProductType { Name = _.Key, Products = _.Select(p=>p) }).ToListAsync();
            }
            
            return await Task.FromResult(View(productTypes));
        }
    }
}