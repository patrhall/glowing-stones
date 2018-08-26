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
    public class PageController : Controller
    {
        public async Task<ActionResult> Index(string id)
        {
            var context = new ApplicationDbContext();
            var page = await context.Page.FirstOrDefaultAsync(x => x.Name == id);
            return View(page);
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var context = new ApplicationDbContext();
            var pages = context.Page.ToList();
            var menu = CreateMenuItem(pages);

            return PartialView("_Menu", menu); 
        }

        public IEnumerable<MenuItem> CreateMenuItem(IEnumerable<Page> pages, int parentId = 0)
        {
            return pages.Where(x=>!(x.ParentId.HasValue) || 
            (x.ParentId.HasValue && x.ParentId == parentId)).Select(x => new MenuItem
            {
                Id = x.Id,
                Name = x.Name,
                ParentId = x.ParentId,
                Children = CreateMenuItem(pages.Where(p => x.Id == p.ParentId), x.Id)
            }).ToList();
        }
    }
}