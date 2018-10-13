using Ruby.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Ruby.Extensions;

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

            var currency = "sek";
            var _httpClient = new HttpClient();
            var data =
                "{\"rates\":{\"USDEUR\":{\"rate\":0.864686,\"timestamp\":1539372568362},\"USDSEK\":{\"rate\":8.955691,\"timestamp\":1539372572081}},\"code\":200}"; //;await _httpClient.GetStringAsync("https://www.freeforexapi.com/api/live?pairs=USDEUR,USDSEK");
            var json = JsonConvert.DeserializeObject<RateObject>(data);
            float rate = 1;

            if (currency == "sek")
            {
                rate = json.rates.USDSEK.rate;
            }
            else if (currency == "eur")
            {
                rate = json.rates.USDEUR.rate;
            }

            ViewBag.SEK = json.rates.USDSEK.rate.ToString("##.##");
            ViewBag.EUR = json.rates.USDEUR.rate.ToString("##.##"); ;

            if (!string.IsNullOrWhiteSpace(name))
            {
                products = await context.Product.Where(x => x.Name == name && x.Color == color).AndCurrencyRate(currency, rate).ToListAsync();
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