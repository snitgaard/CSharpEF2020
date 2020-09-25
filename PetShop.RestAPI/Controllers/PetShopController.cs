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
    public class PetShopController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetShopController(IPetService petService)
        {
            _petService = petService;
        }
        /// <summary>
        /// Returns list of all pets as JSON
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get()
        {
            try
            {
                return Ok(_petService.GetPets());
            }
            catch (Exception)
            {
                return StatusCode(500, "Couldn't get the Pets... Try again, perhaps?");
            }
        }

        /// <summary>
        /// Returns pet with the ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Route("[action]/{id}")]
        public ActionResult<Pet> Get(int id)
        {
            var foundPet = _petService.FindPetById(id);
            if (foundPet == null)
            {
                return StatusCode(404, "Pet was not found");
            }
            try
            {
                return foundPet;
            }
            catch (Exception)
            {
                return StatusCode(500, "When will you learn that something went wrong?");
            }
        }

        /// <summary>
        /// Returns the Pet that was created
        /// </summary>
        /// <param name="pet"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            if(string.IsNullOrEmpty(pet.Name))
            {
                return StatusCode(500, "Name is required for creating a pet");
            }

            return _petService.CreatePet(pet); 
        }


        /// <summary>
        /// Returns the Pet that was updated
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pet"></param>
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet pet)
        {
            var updatePet = _petService.UpdatePet(pet);
            if (updatePet == null)
            {
                return StatusCode(404, "Pet was not found");
            }

            try
            {
                return updatePet;
            }
            catch (Exception)
            {
                return StatusCode(500, "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            }
        }

        /// <summary>
        /// Returns the pet deleted
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            var deletePet = _petService.DeletePet(id);
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
                return StatusCode(500, "Nope.");
            }
        }

        [HttpGet("{type}")]
        [Route("[action]/{type}")]
        public ActionResult<Pet> GetFilteredPets(string type)
        {
            var pet = _petService.GetAllByType(type); ;

            try
            {
                return Ok(pet);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went horribly wrong during execution. I don't know what to tell you.");
            }
        }
    }
}
