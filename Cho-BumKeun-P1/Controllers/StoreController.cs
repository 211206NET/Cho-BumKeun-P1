using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using CustomExceptions;

namespace Cho_BumKeun_P1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private IBL _bl;
        public StoreController(IBL bl)
        {
            _bl = bl;
        }

        /// <summary>
        /// Shows list of all stores
        /// </summary>
        /// <returns>List of stores</returns>
        // GET: api/<StoreController>
        [HttpGet]
        public List<Store> Get()
        {
            return _bl.GetAllStores();
        }

        // GET api/<StoreController>/5
        //[HttpGet("{id}")]
        //public ActionResult<Store> Get(int id)
        //{
        //   Store foundSto = _bl.GetStoreById(id);
        //   if (foundSto.Id != 0)
        //   {
        //       return Ok(foundSto);
        //   }
        //   else
        //   {
        //       return NoContent();
        //   }
        //}

        /// <summary>
        /// Gets all store orders with sort selection
        /// </summary>
        /// <param name="storeId">int store ID</param>
        /// <param name="select">string selection choice</param>
        /// <returns>Sorted list of all store orders</returns>
        // GET api/<StoreController>/5
        [HttpGet("{storeId}")]
        public ActionResult<List<Order>> Get(int storeId, string select)
        {
            if (select == "old")
            {
                List<Order> allOrders = _bl.GetAllOrdersStoreDateON(storeId);
                return Ok(allOrders);
            }
            else if (select == "new")
            {
                List<Order> allOrders = _bl.GetAllOrdersStoreDateNO(storeId);
                return Ok(allOrders);
            }
            else if (select == "low")
            {
                List<Order> allOrders = _bl.GetAllOrdersStorePriceLH(storeId);
                return Ok(allOrders);
            }
            else if (select == "high")
            {
                List<Order> allOrders = _bl.GetAllOrdersStorePriceHL(storeId);
                return Ok(allOrders);
            }
            else
            {
                return BadRequest();
;           }
        }
    }
}
