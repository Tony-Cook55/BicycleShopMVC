using Microsoft.AspNetCore.Mvc;
using MyShop.Models;



namespace MyShop.Controllers
{
    public class ShoppingCartController : Controller
    {


        // Hit the database
        private readonly ShopContext _context;

        public ShoppingCartController(ShopContext context)
        {
            _context = context;
        }




        public IActionResult Index()
        {
            return View();
        }





        // +++++++++++++++++++++++++++++++++ ADD ITEM +++++++++++++++++++++++++++++++++ \\
        [HttpGet]
        public IActionResult AddToCart(int id) // Receives the Id of the bicycle
        {
            var bicycleToAdd = _context.Bicycles.Find(id);


            // Reassigning the cart items and reading the cart items out of the session but if there null ?? then create a new list of Shopping cart items
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();


            // This will return an item or not based on the first item it find using the Bicycle Id
            var existingCartItem = cartItems.FirstOrDefault(item => item.Bicycle.Id == id);

            // If the item exists in the cart add to the Quantity of the existing item in cart
            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
            }
            else // If item id is null and doesn't exist add the new Item with a Quantity of 1
            {
                cartItems.Add(new ShoppingCartItem
                {
                    Bicycle = bicycleToAdd,
                    Quantity = 1
                });
            }


            // You Must Add These in Program.cs
            /*
                // Add these to allow Server Side Sessions to store States in
                builder.Services.AddMemoryCache();
                builder.Services.AddSession();

            // Enables the Sessions on the Server
            app.UseSession();

            */
            HttpContext.Session.Set("Cart", cartItems); // NEED SessionExtensions.cs To allow this item to be set in the Session

            // Writing TempData to be Read in a ViewBag
            TempData["CartMessage"] = $"{bicycleToAdd.Brand} {bicycleToAdd.Model} added cart";


            return RedirectToAction("ViewCart");
        }
        // +++++++++++++++++++++++++++++++++ ADD ITEM +++++++++++++++++++++++++++++++++ \\




        [HttpGet]
        public IActionResult ViewCart()
        {

            // Reassigning the cart items and reading the cart items out of the session but if there null ?? then create a new list of Shopping cart items
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            var cartViewModel = new ShoppingCartViewModel
            {
                CartItems = cartItems,
                TotalPrice = cartItems.Sum(item => item.Bicycle.Price * item.Quantity)
            };


            // Allows Us to Call in the ViewBag with the TempData Message we made above while adding the Item
            ViewBag.CartMessage = TempData["CartMessage"]; // Reading TempData

            return View(cartViewModel);
        }




        // ------------------------------ DELETE ITEM ------------------------------ \\
        public IActionResult RemoveItem(int id)
        {
            // Reassigning the cart items and reading the cart items out of the session but if there null ?? then create a new list of Shopping cart items
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            // Look up the item to remove by its id
            var itemToRemove = cartItems.FirstOrDefault(item => item.Bicycle.Id == id);

            // If the item to remove is NOT null
            if (itemToRemove != null)
            {
                // IF there is more than one item with more than 1 for the quantity simply -1 from that total quantity   Ex: (Blue Bike: 2 Total) --> (Blue Bike: 1 Total)
                if(itemToRemove.Quantity > 1)
                {
                    itemToRemove.Quantity--;
                    TempData["CartMessage"] = $"{itemToRemove.Bicycle.Brand} {itemToRemove.Bicycle.Model} Removed From Cart";

                }
                else // If its only 1 quantity completely remove it
                {
                    cartItems.Remove(itemToRemove);
                    TempData["CartMessage"] = $"{itemToRemove.Bicycle.Brand} {itemToRemove.Bicycle.Model} Removed From Cart";
                }
            }

            // Reset the Cart with the cart items
            HttpContext.Session.Set("Cart", cartItems);


            return RedirectToAction("ViewCart"); // Go back to the ViewCart Method

        }
        // ------------------------------ DELETE ITEM ------------------------------ \\




        // [][][][][][][][][][][][][][] BUYING ITEM [][][][][][][][][][][][][][] \\
        public IActionResult PurchaseItems()
        {
            // Reassigning the cart items and reading the cart items out of the session but if there null ?? then create a new list of Shopping cart items
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new List<ShoppingCartItem>();

            foreach(var item in cartItems)
            {
                // Saving each item as a Purchase
                _context.Purchase.Add(new Purchase
                {
                    BicycleId = item.Bicycle.Id,
                    Quantity = item.Quantity,
                    PurchaseDate = DateTime.Now,
                    Total = item.Bicycle.Price * item.Quantity,
                });
            }

            // Save Changes into the Database
            _context.SaveChanges();

            // Clear the Cart
            HttpContext.Session.Set("Cart", new List<ShoppingCartItem>());

            return RedirectToAction("Index", "Home");
        }
        // [][][][][][][][][][][][][][] BUYING ITEM [][][][][][][][][][][][][][] \\

    }
}
