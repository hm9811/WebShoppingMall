using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using WebShoppingMall.Data;
using WebShoppingMall.Models;

namespace WebShoppingMall.Controllers
{
    public class ProductListsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _he;

        public ProductListsController(ApplicationDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: ProductLists
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.ProductTag).Include(p => p.ProductTypes);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products = _context.Products.Include(c => c.ProductTypes).Include(c => c.ProductTag)
                .Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();
            if (lowAmount == null || largeAmount == null)
            {
                products = _context.Products.Include(c => c.ProductTypes).Include(c => c.ProductTag).ToList();
            }
            return View(products);
        }

        // GET: ProductLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productList = await _context.Products
                .Include(p => p.ProductTag)
                .Include(p => p.ProductTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productList == null)
            {
                return NotFound();
            }

            return View(productList);
        }

        // GET: ProductLists/Create
        public IActionResult Create()
        {
            ViewData["TagId"] = new SelectList(_context.TagProduct, "TagId", "Name");
            ViewData["ProductId"] = new SelectList(_context.PhotoProducts, "ProductId", "Title");
            return View();
        }

        // POST: ProductLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Price,Image,Color,IsAvailable,ProductId,TagId")] ProductList productList, IFormFile image)
        {
            if (ModelState.IsValid)
            {

                var uniqueProduct = _context.Products.FirstOrDefault(c => c.Title == productList.Title);
                if (uniqueProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                    ViewData["TagId"] = new SelectList(_context.TagProduct, "TagId", "Name", productList.TagId);
                    ViewData["ProductId"] = new SelectList(_context.PhotoProducts, "ProductId", "Title", productList.ProductId);
                    return View(productList);
                }

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    productList.Image ="Images/" + image.FileName;
                }
                if (image == null)
                {
                    productList.Image = "Images/noimage.PNG";
                }
                _context.Add(productList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(productList);
        }

        // GET: ProductLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productList = await _context.Products.FindAsync(id);
            if (productList == null)
            {
                return NotFound();
            }
            ViewData["TagId"] = new SelectList(_context.TagProduct, "TagId", "TagId", productList.TagId);
            ViewData["ProductId"] = new SelectList(_context.PhotoProducts, "ProductId", "ProductId", productList.ProductId);
            return View(productList);
        }

        // POST: ProductLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Price,Image,Color,IsAvailable,ProductId,TagId")] ProductList productList, IFormFile image)
        {
            if (id != productList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null)
                    {
                        var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                        await image.CopyToAsync(new FileStream(name, FileMode.Create));
                        productList.Image = "Images/" + image.FileName;
                    }
                    if (image == null)
                    {
                        productList.Image = "Images/noimage.PNG";
                    }

                    _context.Update(productList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductListExists(productList.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TagId"] = new SelectList(_context.TagProduct, "TagId", "TagId", productList.TagId);
            ViewData["ProductId"] = new SelectList(_context.PhotoProducts, "ProductId", "ProductId", productList.ProductId);
            return View(productList);
        }

        // GET: ProductLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productList = await _context.Products
                .Include(p => p.ProductTag)
                .Include(p => p.ProductTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productList == null)
            {
                return NotFound();
            }

            return View(productList);
        }

        // POST: ProductLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productList = await _context.Products.FindAsync(id);
            _context.Products.Remove(productList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductListExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
