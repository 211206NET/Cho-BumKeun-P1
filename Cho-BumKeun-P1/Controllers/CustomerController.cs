using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using CustomExceptions;
using Microsoft.Extensions.Caching.Memory;

namespace Cho_BumKeun_P1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IBL _bl;
        private IMemoryCache _memoryCache;
        public CustomerController(IBL bl, IMemoryCache memoryCache)
        {
            _bl = bl;
            _memoryCache = memoryCache;
        }

        // GET: api/<CustomerController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<CustomerController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<CustomerController>
        [HttpPost]
        public ActionResult Post([FromBody] Customer customerToAdd)
        {
            try
            {
                _bl.AddCustomer(customerToAdd);
                return Created("Successfully signed up", customerToAdd);
            }
            catch (DuplicateRecordException ex)
            {
                return Conflict(ex.Message);
            }
        }

        // GET api/<StoreController>/5
        [HttpGet("{userId}")]
        public ActionResult<List<Order>> Get(int userId, string select)
        {
            if (select == "old")
            {
                List<Order> allOrders = _bl.GetAllOrdersDateON(userId);
                return Ok(allOrders);
            }
            else if (select == "new")
            {
                List<Order> allOrders = _bl.GetAllOrdersDateNO(userId);
                return Ok(allOrders);
            }
            else if (select == "low")
            {
                List<Order> allOrders = _bl.GetAllOrdersPriceLH(userId);
                return Ok(allOrders);
            }
            else if (select == "high")
            {
                List<Order> allOrders = _bl.GetAllOrdersPriceHL(userId);
                return Ok(allOrders);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/<CustomerController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<CustomerController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
