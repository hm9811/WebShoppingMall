using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShoppingMall.Data;
using WebShoppingMall.Models;
using WebShoppingMall.Utility;

namespace WebShoppingMall.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order orders)
        {
            List<ProductList> products = HttpContext.Session.Get<List<ProductList>>("products");
            if (products != null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.PorductId = product.Id;
                    orders.OrderDetails.Add(orderDetails);
                }
            }

            orders.OrderNo = GetOrderNo();
            _context.Order.Add(orders);
            await _context.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<ProductList>());
            return View();
        }

        public string GetOrderNo()
        {
            int rowCount = _context.Order.ToList().Count() + 1;
            return rowCount.ToString("000");
        }
    }
}
