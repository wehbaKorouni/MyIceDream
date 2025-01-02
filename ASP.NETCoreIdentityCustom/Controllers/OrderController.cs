using System.Net;
using MyIceDream.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using MyIceDream.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;


namespace MyIceDream.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }






        //[Authorize(Policy = "RequireAdmin")]
        public ActionResult Index()
        {
            var orders = _context.Orders.ToList();
            return View(orders);
        }

        public IActionResult Create()
        {
           
            return View();
        }

        // POST: Order/Create
        [HttpPost]
      
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {

                // Assign the currently logged-in user's ID to the Order
                //order.UserId = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                //// You might want to calculate the TotalPrice here based on ProductId and Quantity
                //var product = _context.Products.FirstOrDefault(p => p.Id == order.ProductId);
                //if (product != null)
                //{
                //    order.TotalPrice = product.Price * order.Quantity; // Assuming product has a 'Price' property
                //}

                // Add the new order to the context
                _context.Orders.Add(order);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); // Redirect to the list of orders after creation
            }

            // If the model state is not valid, return to the Create view with the current order
            //var products = _context.Products.ToList(); // Repopulate the product list
            //ViewBag.Products = new SelectList(products, "Id", "Name"); // Repopulate product selection list
            return View(order);
        }

    }
}
