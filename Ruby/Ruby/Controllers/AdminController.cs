using OfficeOpenXml;
using Ruby.Extensions;
using Ruby.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ruby.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        public ActionResult ImportProducts()
        {
            return View();
        }

        public ActionResult ImportImages()
        {
            return View();
        }

        public async Task<ActionResult> EditPage(int id)
        {
            var context = new ApplicationDbContext();
            var page = await context.Page.FindAsync(id);

            return View(page ?? new Page());
        }

        public ActionResult AddPage(int? parentId = null)
        {
            var page = new Page { ParentId = parentId };
            return View(page);
        }

        public async Task<ActionResult> Pages()
        {
            var context = new ApplicationDbContext();
            var pages = await context.Page.ToListAsync();
            return View(pages);
        }

        public async Task<ActionResult> ExcelImport(FormCollection formCollection)
        {
            if (Request != null)
            {
                var context = new ApplicationDbContext();
                HttpPostedFileBase file = Request.Files["UploadedFile"];

                if ((file != null) && (file.ContentLength > 0) && (Path.GetExtension(file.FileName) == ".xlsx"))
                {
                    ExcelPackage package = new ExcelPackage(file.InputStream);

                    foreach (var workSheet in package.Workbook.Worksheets.Where(c=>c.Cells.First().Text == "Lot number"))
                    {
                        Dictionary<int, string> cells = new Dictionary<int, string>();
                        int cellCount = 0;
                        foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
                        {
                            cells.Add(cellCount, firstRowCell.Text);
                            cellCount += 1;
                        }

                        var products = new List<Product>();
                        var updateProducts = new List<Product>();

                        for (var rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
                        {
                            int cellInRows = 0;
                            var product = new Product();
                            bool addProduct = true;
                            bool nextWorksheet = false;
                            foreach (var cell in cells)
                            {
                                var keyName = cell.Value;
                                var excelCellValue = (workSheet.Cells[rowNumber, cell.Key+1].Value ?? "").ToString();
                                if (keyName == "Lot number")
                                {
                                    if (string.IsNullOrWhiteSpace(excelCellValue))
                                    {
                                        nextWorksheet = true;
                                        break;
                                    }

                                    var exist = await context.Product.FirstOrDefaultAsync(x => x.InternalId == excelCellValue);
                                    if(exist!=null)
                                    {
                                        addProduct = false;
                                        product = exist;
                                    }
                                }

                                product.SetByDisplayName(keyName, excelCellValue);

                                cellInRows += 1;

                            }
                            if (!nextWorksheet)
                            {
                                if (addProduct)
                                {
                                    products.Add(product);
                                }
                                else
                                {
                                    await context.SaveChangesAsync();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }

                        context.Product.AddRange(products);
                        await context.SaveChangesAsync();
                    }
                }
            }
            ViewBag.Result = "Done";
            return View("ImportProducts");
        }

        public async Task<ActionResult> ImageImport(HttpPostedFileBase[] files)
        {
            if (Request != null)
            {
                var fileTypes = new List<string> { ".jpg", ".png" , ".jpeg" , ".gif" };
                var context = new ApplicationDbContext();
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0 && fileTypes.Contains(Path.GetExtension(file.FileName)))
                    {
                        byte[] data;
                        using (Stream inputStream = file.InputStream)
                        {
                            MemoryStream memoryStream = inputStream as MemoryStream;
                            if (memoryStream == null)
                            {
                                memoryStream = new MemoryStream();
                                inputStream.CopyTo(memoryStream);
                            }
                            data = memoryStream.ToArray();

                            if (data != null)
                            {
                                var fileName = Path.GetFileNameWithoutExtension(file.FileName).ToLower();
                                var product = await context.Product.SingleOrDefaultAsync(_=>_
                                .InternalId.ToLower() == fileName);

                                product.Image = data;
                                product.ThumbImage = MakeThumbnail(data);
                            }
                        }
                    }

                }
                await context.SaveChangesAsync();
            }
            
            ViewBag.Result = "Done";
            return View("ImportImages");
        }

        public static byte[] MakeThumbnail(byte[] myImage)
        {
            int width = 250;
            using (MemoryStream ms = new MemoryStream())
            using (Image image = Image.FromStream(new MemoryStream(myImage)))
            {
                if (image.Width > width)
                {
                    int height = image.Height / (image.Width / width);
                    var thumbnail = image.GetThumbnailImage(width, height, null, new IntPtr());
                    thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
                return myImage;
            }
        }

        public async Task<ActionResult> SavePage(int id, string name, string pageContent, int? parentId = null)
        {
            var context = new ApplicationDbContext();
            
            if (id > 0)
            {
                var page = await context.Page.FindAsync(id);
                page.PageContent = pageContent;
                page.Name = name;
            }
            else
            {
                var page = new Page {
                    PageContent = pageContent,
                    Name = name,
                    ParentId = parentId
                };
                context.Page.Add(page);
            }
            
            await context.SaveChangesAsync();

            return RedirectToAction("Pages");
        }
    }
}