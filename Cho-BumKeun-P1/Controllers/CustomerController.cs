using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using CustomExceptions;

namespace Cho_BumKeun_P1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IBL _bl;
        public CustomerController(IBL bl)
        {
            _bl = bl;
        }

        /// <summary>
        /// Sign up username and password
        /// </summary>
        /// <param name="customerToAdd">customerToAdd object</param>
        /// <returns>Created object or exception message</returns>
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

        /// <summary>
        /// Check and login if username and password match
        /// </summary>
        /// <param name="username">string username</param>
        /// <param name="password">string password</param>
        /// <returns>Success or fail</returns>
        // GET: api/<StoreController>
        [HttpGet]
        public ActionResult<Customer> Get(string username, string password)
        {
            Customer existing = _bl.Login(new Customer {UserName = username, Password = password});
            if (existing.Id <= 0)
            {
                return BadRequest("User does not exist");
            }
            else
            {
                if (existing.Password == password)
                {
                    return Ok("You've successfully logged in");
                }
                else
                {
                    return BadRequest("Incorrect password");
                }
            }
        }

        /// <summary>
        /// Gets all user's orders with sort selection
        /// </summary>
        /// <param name="userId">int user ID</param>
        /// <param name="select">string selection choice</param>
        /// <returns>Sorted list of all user's orders</returns>
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
    }
}
