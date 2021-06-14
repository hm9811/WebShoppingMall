using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebShoppingMall.Data;
using WebShoppingMall.Models;

namespace WebShoppingMall.Controllers
{
    [Authorize(Roles = IdentityHelper.Admin)]
    public class PhotoProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhotoProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PhotoProducts
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhotoProducts.ToListAsync());
        }

        // GET: PhotoProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoProduct = await _context.PhotoProducts
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (photoProduct == null)
            {
                return NotFound();
            }

            return View(photoProduct);
        }

        // GET: PhotoProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhotoProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Title")] PhotoProduct photoProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(photoProduct);
                await _context.SaveChangesAsync();
                TempData["save"] = "Product type has been saved";
                return RedirectToAction(nameof(Index));
            }
            return View(photoProduct);
        }

        // GET: PhotoProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoProduct = await _context.PhotoProducts.FindAsync(id);
            if (photoProduct == null)
            {
                return NotFound();
            }
            return View(photoProduct);
        }

        // POST: PhotoProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Title")] PhotoProduct photoProduct)
        {
            if (id != photoProduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photoProduct);
                    await _context.SaveChangesAsync();
                    TempData["edit"] = "Product type has been updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoProductExists(photoProduct.ProductId))
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
            return View(photoProduct);
        }

        // GET: PhotoProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoProduct = await _context.PhotoProducts
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (photoProduct == null)
            {
                return NotFound();
            }

            return View(photoProduct);
        }

        // POST: PhotoProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photoProduct = await _context.PhotoProducts.FindAsync(id);
            _context.PhotoProducts.Remove(photoProduct);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Product type has been deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoProductExists(int id)
        {
            return _context.PhotoProducts.Any(e => e.ProductId == id);
        }
    }
}
