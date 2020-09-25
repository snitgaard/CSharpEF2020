using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.ApplicationServices;
using PetShop.Core.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetShop.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }
        // GET: api/<PetShopController>
        [HttpGet]
        public ActionResult<IEnumerable<Owner>> Get()
        {
            try
            {
                return Ok(_ownerService.GetAllOwners());
            }
            catch (Exception)
            {
                return StatusCode(500, "Something is not right..");
            }
        }

        // GET api/<PetShopController>/5
        [HttpGet("{id}")]
        [Route("[action]/{id}")]
        public ActionResult<Owner> Get(int id)
        {
            var foundOwner = _ownerService.FindOwnerById(id);
            if (foundOwner == null)
            {
                return StatusCode(404, "Owner was not found");
            }

            try
            {
                return foundOwner;
            }
            catch (Exception e)
            {
                return StatusCode(500, "Yes it sucks. Deal with it");
            }
        }

        // POST api/<PetShopController>
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner owner)
        {
            if (string.IsNullOrEmpty(owner.Name))
            {
                return StatusCode(500, "Name is required for creating a owner");
            }

            if (string.IsNullOrEmpty(owner.Address))
            {
                return StatusCode(500, "Address is required for creating an owner");
            }
            return _ownerService.CreateOwner(owner);
        }

        // PUT api/<PetShopController>/5
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner owner)
        {
            var updateOwner = _ownerService.UpdateOwner(owner);
            if (updateOwner == null)
            {
                return StatusCode(404, "Owner was not found");
            }

            try
            {
                return updateOwner;
            }
            catch (Exception)
            {
                return StatusCode(500, "Shit happens.");
            }
        }

        // DELETE api/<PetShopController>/5
        [HttpDelete("{id}")]
        public ActionResult<Owner> Delete(int id)
        {
            var deleteOwner = _ownerService.DeleteOwner(id);
            if (deleteOwner == null)
            {
                return StatusCode(404, "Owner was not found");
            }
            try
            {
                return deleteOwner;
            }
            catch (Exception)
            {
                return StatusCode(500, "Did not work.");
            }
        }

        [HttpGet("{name}")]
        [Route("[action]/{name}")]
        public ActionResult<List<Owner>> GetFilteredOwners(string name)
        {
            var owner = _ownerService.GetAllByName(name);
            try
            {
                return Ok(owner);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went horribly wrong during execution. Pathetic.");
            }
        }
    }
}
