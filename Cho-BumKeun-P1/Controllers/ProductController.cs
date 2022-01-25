using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using CustomExceptions;

namespace Cho_BumKeun_P1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IBL _bl;
        public ProductController(IBL bl)
        {
            _bl = bl;
        }

        /// <summary>
        /// Shows list of all products
        /// </summary>
        /// <returns>List of products</returns>
        // GET: api/<ProductController>
        [HttpGet]
        public List<Product> Get()
        {
            return _bl.GetAllProducts();
        }

        
        /// <summary>
        /// Search product by ID
        /// </summary>
        /// <param name="id">int product ID</param>
        /// <returns>Product object</returns>
        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            Product foundProd = _bl.GetProductById(id);
            if(foundProd.Id != 0)
            {
                return Ok(foundProd);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Replenish product inventory
        /// </summary>
        /// <param name="username">string admin username</param>
        /// <param name="password">string admin password</param>
        /// <returns>Success message or BadRequest</returns>
        // PUT api/<ProductController>/5
        [HttpPut("{username}")]
        public ActionResult Put(string username, string password)
        {
            if (username == "vaporadmin")
            {
                if (password == "password")
                {
                    _bl.ReplenishInventory();
                    return Ok("Inventory has been replenished");
                }
                else
                {
                    return BadRequest("Incorrect admin password");
                }
            }
            else
            {
                return BadRequest("Incorrect admin username");
            }
        }
    }
}
