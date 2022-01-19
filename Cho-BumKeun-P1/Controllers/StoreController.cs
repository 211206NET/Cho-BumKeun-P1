using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using CustomExceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        // GET: api/<StoreController>
        [HttpGet]
        public List<Store> Get()
        {
            return _bl.GetAllStores();
            //return new List<Store>(){
            //    new Store() {
            //        Name = "Avon Commons Vapor",
            //        City = "Avon",
            //        State = "IN"
            //   },
            //    new Store() {
            //        Name = "Garwood Mall Vapor",
            //       City = "Garwood",
            //        State = "NJ"
            //    }
            //};
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StoreController>
        [HttpPost]
        public ActionResult Post([FromBody] Customer customerToAdd)
        {
            try
            {
                _bl.AddCustomer(customerToAdd);
                return Created("Successfully added", customerToAdd);
            }
            catch (DuplicateRecordException ex)
            {
                return Conflict(ex.Message);
            }
        }

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
