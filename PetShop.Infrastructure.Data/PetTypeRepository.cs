using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetShop.Core.DomainServices;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Data
{
    public class PetTypeRepository: IPetTypeRepository
    {
        static int id = 1;
        static List<PetType> _petTypes = new List<PetType>();

        public PetType Create(PetType petType)
        {
            petType.Id = id++;
            _petTypes.Add(petType);
            return petType;
        }

        public PetType ReadById(int id)
        {
            foreach (var petType in _petTypes)
            {
                if (petType.Id == id)
                {
                    return petType;
                }
            }
            return null;
        }

        public IEnumerable<PetType> ReadPetTypes()
        {
            return _petTypes;
        }

        public PetType Update(PetType petTypeUpdate)
        {
            var petTypeDB = this.ReadById(petTypeUpdate.Id);
            if (petTypeDB != null)
            {
                petTypeDB.Type= petTypeUpdate.Type;
            }
            return null;
        }

        public PetType Delete(int id)
        {
            var petTypeFound = this.ReadById(id);
            if (petTypeFound != null)
            {
                _petTypes.Remove(petTypeFound);
                return petTypeFound;
            }
            return null;
        }
    }
}
