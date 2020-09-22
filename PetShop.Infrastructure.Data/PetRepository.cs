using PetShop.Core.DomainServices;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastructure.Data
{
    public class PetRepository : IPetRepository
    {
        static int id = 1;
        static List<Pet> _pets = new List<Pet>();

        public Pet Create(Pet pet)
        {
            pet.Id = id++;
            _pets.Add(pet);
            return pet;
        }
        public Pet ReadById(int id)
        {
            foreach (var pet in _pets)
            {
                if (pet.Id == id)
                {
                    return pet;
                }
            }

            return null;
        }

        public IEnumerable<Pet> ReadPets()
        {
            return _pets;
        }
        public Pet Update(Pet petUpdate)
        {
            var petDB = this.ReadById(petUpdate.Id);
            if (petDB != null)
            {
                petDB.Name = petUpdate.Name;
                petDB.Type = petUpdate.Type;
                petDB.BirthDate = petUpdate.BirthDate;
                petDB.SoldDate = petUpdate.SoldDate;
                petDB.Color = petUpdate.Color;
                petDB.PreviousOwner = petUpdate.PreviousOwner;
                petDB.Price = petUpdate.Price;
            }

            return null;
        }

        public Pet Delete(int id)
        {
            var petFound = this.ReadById(id);
            if (petFound != null)
            {
                _pets.Remove(petFound);
                return petFound;
            }

            return null;
        }

    }
}
