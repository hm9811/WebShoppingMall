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
    public class TagProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TagProducts
        public async Task<IActionResult> Index()
        {
            return View(await _context.TagProduct.ToListAsync());
        }

        // GET: TagProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagProduct = await _context.TagProduct
                .FirstOrDefaultAsync(m => m.TagId == id);
            if (tagProduct == null)
            {
                return NotFound();
            }

            return View(tagProduct);
        }

        // GET: TagProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TagProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TagId,Name")] TagProduct tagProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tagProduct);
                await _context.SaveChangesAsync();
                TempData["save"] = "Product type has been saved";
                return RedirectToAction(nameof(Index));
            }
            return View(tagProduct);
        }

        // GET: TagProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagProduct = await _context.TagProduct.FindAsync(id);
            if (tagProduct == null)
            {
                return NotFound();
            }
            return View(tagProduct);
        }

        // POST: TagProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TagId,Name")] TagProduct tagProduct)
        {
            if (id != tagProduct.TagId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tagProduct);
                    await _context.SaveChangesAsync();
                    TempData["edit"] = "Product type has been updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagProductExists(tagProduct.TagId))
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
            return View(tagProduct);
        }

        // GET: TagProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagProduct = await _context.TagProduct
                .FirstOrDefaultAsync(m => m.TagId == id);
            if (tagProduct == null)
            {
                return NotFound();
            }

            return View(tagProduct);
        }

        // POST: TagProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tagProduct = await _context.TagProduct.FindAsync(id);
            _context.TagProduct.Remove(tagProduct);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Product type has been deleted";
            return RedirectToAction(nameof(Index));
        }

        private bool TagProductExists(int id)
        {
            return _context.TagProduct.Any(e => e.TagId == id);
        }
    }
}
