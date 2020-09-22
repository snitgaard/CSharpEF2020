using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetShop.Core.DomainServices;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationServices.Services
{
    public class PetTypeService : IPetTypeService
    {
        private readonly IPetTypeRepository _petTypeRepo;
        private readonly IPetRepository _petRepo;

        public PetTypeService(IPetTypeRepository petTypeRepository, IPetRepository petRepository)
        {
            _petTypeRepo = petTypeRepository;
            _petRepo = petRepository;
        }

        public PetType NewPetType(string type)
        {
            var petType = new PetType()
            {
                Type = type
            };
            return petType;
        }

        public PetType CreatePetType(PetType petType)
        {
            return _petTypeRepo.Create(petType);
        }

        public List<PetType> GetPetTypes()
        {
            return _petTypeRepo.ReadPetTypes().ToList();
        }

        public PetType FindPetTypeById(int id)
        {
            return _petTypeRepo.ReadById(id);
        }

        public PetType UpdatePetType(PetType updatePetType)
        {
            var petType = FindPetTypeById(updatePetType.Id);
            petType.Type = updatePetType.Type;
            return petType;
        }

        public PetType DeletePetType(int id)
        {
            return _petTypeRepo.Delete(id);
        }

        public List<PetType> GetAllByType(string type)
        {
            var list = _petTypeRepo.ReadPetTypes();
            var query = list.Where(pettype => pettype.Type.Equals(type));
            query.OrderBy(pettype => pettype.Type);
            return query.ToList();
        }
    }
}
