using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebShoppingMall.Data;
using WebShoppingMall.Models;
using WebShoppingMall.Utility;

namespace WebShoppingMall.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Products.Include(p => p.ProductTypes).Include(p => p.ProductTag).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productList = await _context.Products
                .Include(p => p.ProductTypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productList == null)
            {
                return NotFound();
            }
            return View(productList);
        }

        [HttpPost]
        [ActionName("Detail")]
        public ActionResult ProductDetail(int? id)
        {
            List<ProductList> products = new List<ProductList>();
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            products = HttpContext.Session.Get<List<ProductList>>("products");
            if (products == null)
            {
                products = new List<ProductList>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Cart()
        {
            List<ProductList> products = HttpContext.Session.Get<List<ProductList>>("products");
            if (products == null)
            {
                products = new List<ProductList>();
            }
            return View(products);
        }

        public IActionResult Remove(int? id)
        {
            List<ProductList> products = HttpContext.Session.Get<List<ProductList>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [ActionName("Remove")]

        public IActionResult RemoveConfirm(int? id)
        {
            List<ProductList> products = HttpContext.Session.Get<List<ProductList>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Cart));
        }
    }
}
