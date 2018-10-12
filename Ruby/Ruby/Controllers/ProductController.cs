using Ruby.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ruby.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult Index(bool start = false)
        {
            var context = new ApplicationDbContext();
            var  productTypes = context.Product.GroupBy(_=>_.Name)
                    .OrderByDescending(_=>_.Count())
                    .Select(_=>new ProductType { Name = _.Key, Products = _.GroupBy(g=>g.Color).Select(p=>p.FirstOrDefault()) })
                    .ToList();

            if (start)
            {
                return PartialView(productTypes);
            }
            else
            {
                return View(productTypes);
            }
        }

        public async Task<ActionResult> Shop(string name, string color = null)
        {
            var context = new ApplicationDbContext();
            var products = new List<Product>();

            color = color == "" ? null : color;

            var _httpClient = new HttpClient();
            var data = await _httpClient.GetStringAsync("https://www.freeforexapi.com/api/live?pairs=USDEUR,USDSEK");

            if (!string.IsNullOrWhiteSpace(name))
            {
                products = await context.Product.Where(x => x.Name == name && x.Color == color).ToListAsync();
            }

            return await Task.FromResult(View(products));
        }

        public async Task<ActionResult> Cart()
        {
            return View();
        }

        public async Task<ActionResult> GetCart(List<string> ids)
        {
            var context = new ApplicationDbContext();
            var products = await context.Product.Where(x => ids.Contains(x.InternalId)).ToListAsync();

            return await Task.FromResult(PartialView(products));
        }
    }
}