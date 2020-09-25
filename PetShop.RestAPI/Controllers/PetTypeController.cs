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
    public class PetTypeController : ControllerBase
    {
        private readonly IPetTypeService _petTypeService;
        public PetTypeController(IPetTypeService petTypeService)
        {
            _petTypeService = petTypeService;
        }
        // GET: api/<PetTypeController>
        [HttpGet]
        public ActionResult<IEnumerable<PetType>> Get()
        {
            try
            {
                return Ok(_petTypeService.GetPetTypes());
            }
            catch (Exception)
            {
                return StatusCode(500,
                    "Hmm yes, I can sense something is wrong... Not gonna tell you what tho, you'll have to figure it out.");
            }
        }

        // GET api/<PetTypeController>/5
        [HttpGet("{id}")]
        [Route("[action]/{id}")]
        public ActionResult<PetType> Get(int id)
        {
            var petType = _petTypeService.FindPetTypeById(id);

            if (petType == null)
            {
                return StatusCode(404, "Pet was not found.");
            }
            try
            {
                return Ok(petType);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went horribly wrong. Sucks to suck");
            }
        }

        // POST api/<PetTypeController>
        [HttpPost]
        public ActionResult<PetType> Post([FromBody] PetType petType)
        {
            if (string.IsNullOrEmpty(petType.Type))
            {
                return StatusCode(500, "Something went wrong.");
            }
            return _petTypeService.CreatePetType(petType);
        }

        // PUT api/<PetTypeController>/5
        [HttpPut("{id}")]
        public ActionResult<PetType> Put(int id, [FromBody] PetType petType)
        {
            var updatePet = _petTypeService.UpdatePetType(petType);
            if(updatePet == null)
            {
                return StatusCode(404, "Pet was not found");
            }

            try
            {
                return updatePet;
            }
            catch (Exception)
            {
                return StatusCode(500, "Do better");
            }

        }

        // DELETE api/<PetTypeController>/5
        [HttpDelete("{id}")]
        public ActionResult<PetType> Delete(int id)
        {
            var deletePet = _petTypeService.DeletePetType(id);
            if (deletePet == null)
            {
                return StatusCode(404, "Pet was not found");
            }

            try
            {
                return deletePet;
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went horribly wrong");
            }
        }
        [HttpGet("{type}")]
        [Route("[action]/{type}")]
        public ActionResult<PetType> GetFilteredPetTypes(string type)
        {
            var petType= _petTypeService.GetAllByType(type);

            try
            {
                return Ok(petType);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went horribly wrong during execution. Rename please.");
            }
        }

    }
}
