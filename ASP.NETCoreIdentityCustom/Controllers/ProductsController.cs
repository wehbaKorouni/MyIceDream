using Microsoft.AspNetCore.Mvc;
using MyIceDream.Areas.Identity.Data;
using MyIceDream.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyIceDream.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Simulating a database with an in-memory list
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Vanilla Ice Cream", Price = 3.99m },
            new Product { Id = 2, Name = "Chocolate Ice Cream", Price = 4.99m },
            new Product { Id = 3, Name = "Strawberry Ice Cream", Price = 4.49m }
        };

        // GET: Product
        public IActionResult Index()
        {
            var products = _context.Products.ToList();

            return View(products);
        }

        // GET: Product/Details/{id}
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product newProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(newProduct);
            }

        
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Edit/{id}
        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product updatedProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedProduct);
            }

            var existingProduct = products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Delete/{id}
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            products.Remove(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
