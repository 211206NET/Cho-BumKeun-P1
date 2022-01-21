using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using CustomExceptions;
using Microsoft.Extensions.Caching.Memory;

namespace Cho_BumKeun_P1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IBL _bl;
        private IMemoryCache _memoryCache;
        public ProductController(IBL bl, IMemoryCache memoryCache)
        {
            _bl = bl;
            _memoryCache = memoryCache;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public List<Product> Get()
        {
            //List<Product> allProd = _bl.GetAllProducts();
            //_memoryCache.Set("product", allProd, new TimeSpan(0, 0, 30));
            return _bl.GetAllProducts();
        }

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

        // POST api/<ProductController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<ProductController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ProductController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
