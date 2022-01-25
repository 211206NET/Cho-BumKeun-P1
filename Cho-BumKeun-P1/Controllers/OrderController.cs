using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using CustomExceptions;

namespace Cho_BumKeun_P1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IBL _bl;
        public OrderController(IBL bl)
        {
            _bl = bl;
        }

        /// <summary>
        /// Gets all orders by user ID
        /// </summary>
        /// <param name="userId">int user ID</param>
        /// <returns>List of user's orders</returns>
        // GET api/<OrderController>/5 by user ID
        [HttpGet("{userId}")]
        public List<Order> GetUserOrder(int userId)
        {
            return _bl.GetAllOrders(userId);
        }

        /// <summary>
        /// Gets all orders by store ID
        /// </summary>
        /// <param name="storeId">int store ID</param>
        /// <returns>List of orders place for specified store</returns>
        // GET api/OrderController>/5 by store ID
        [HttpGet]
        public List<Order> GetStoreOrder(int storeId)
        {
            return _bl.StoreOrders(storeId);
        }

        /// <summary>
        /// Creates an order and updates product inventory
        /// </summary>
        /// <param name="storeId">int store ID</param>
        /// <param name="productId">int product ID</param>
        /// <param name="storeName">string store name</param>
        /// <param name="productName">string product name</param>
        /// <param name="quantity">int quantity</param>
        /// <param name="price">decimal price</param>
        /// <param name="userId">int user ID</param>
        /// <param name="time">DateTime</param>
        /// <returns>Success or badrequest message</returns>
        // POST api/<OrderController>
        [HttpPost]
        public ActionResult Post(int storeId, int productId, string storeName, string productName, int quantity, decimal price, int userId, DateTime time)
        {
            List<Product> allProducts = _bl.GetAllProducts();
            Product product = allProducts.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                _bl.AddOrder(storeId, productId, storeName, productName, quantity, price, userId, time);
                _bl.UpdateInventory(productId, product.Inventory-quantity);
                return Ok("Order successfully placed");
            }
            else
            {
                return BadRequest("Invalid Product ID");
            }
        }
    }
}
