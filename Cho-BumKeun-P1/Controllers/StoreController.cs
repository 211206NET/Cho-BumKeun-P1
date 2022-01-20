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
    public class StoreController : ControllerBase
    {
        private IBL _bl;
        private IMemoryCache _memoryCache;
        public StoreController(IBL bl, IMemoryCache memoryCache)
        {
            _bl = bl;
            _memoryCache = memoryCache;
        }

        // GET: api/<StoreController>
        [HttpGet]
        public List<Store> Get()
        {
            //List<Store> allSto = _bl.GetAllStores();
            //_memoryCache.Set("store", allSto, new TimeSpan(0,0,30));
            return _bl.GetAllStores();
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public ActionResult<Store> Get(int id)
        {
            Store foundSto = _bl.GetStoreById(id);
            if(foundSto.Id != 0)
            {
                return Ok(foundSto);
            }
            else
            {
                return NoContent();
            }
        }

        // POST api/<StoreController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<StoreController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
