using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using CustomExceptions;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cho_BumKeun_P1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IBL _bl;
        private IMemoryCache _memoryCache;
        public OrderController(IBL bl, IMemoryCache memoryCache)
        {
            _bl = bl;
            _memoryCache = memoryCache;
        }

        //// GET: api/<OrderController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<OrderController>/5 by user ID
        [HttpGet]
        public List<Order> GetUserOrder(int userId)
        {
           //List<Order> allOrd = _bl.GetAllOrders(userId);
           //_memoryCache.Set("order", allOrd, new TimeSpan(0, 0, 30));
            return _bl.GetAllOrders(userId);
        }

        // GET api/OrderController>/5 by store ID
        [HttpGet("{id}")]
        public List<Order> GetStoreOrder(int storeId)
        {
            return _bl.StoreOrders(storeId);
        }

        //[HttpGet("{id}")]
        //public ActionResult<Order> Get(int storeId)
        //{
        //    List<Order> foundOrd = _bl.StoreOrders(storeId);
        //    if (foundOrd.Count != 0)
        //    {
        //        return Ok(foundOrd);
        //    }
        //    else
        //    {
        //        return NoContent();
        //    }
        //}


        // POST api/<OrderController>
        [HttpPost]
        public void Post(int storeId, int productId, string storeName, string productName, int quantity, decimal price, int userId, DateTime time)
        {
            List<Product> allProducts = _bl.GetAllProducts();
            _bl.AddOrder(storeId, productId, storeName, productName, quantity, price, userId, time);
            _bl.UpdateInventory(productId, allProducts[productId].Inventory-quantity);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
