using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetShop.Core.DomainServices;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationServices.Services
{
    public class PetService: IPetService
    {
        private readonly IPetRepository _petRepo;
        private readonly IPetTypeRepository _petTypeRepo;

        public PetService(IPetRepository petRepository, IPetTypeRepository petTypeRepository)
        {
            _petRepo = petRepository;
            _petTypeRepo = petTypeRepository;
        }

        public Pet NewPet(string name, string type, DateTime birthDate, DateTime soldDate, string color,
            string previousOwner, double price)
        {
            var pet = new Pet()
            {
                Name = name,
                Type = type,
                BirthDate = birthDate,
                SoldDate = soldDate,
                Color = color,
                PreviousOwner = previousOwner,
                Price = price
            };
            return pet;
        }

        public Pet CreatePet(Pet pet)
        {
            return _petRepo.Create(pet);
        }

        public List<Pet> GetPets()
        {
            return _petRepo.ReadPets().ToList();
        }

        public List<Pet> GetAllByType(string type)
        {
            var list = _petRepo.ReadPets();
            var query = list.Where(pet => pet.Type.Equals(type));
            query.OrderBy(pet => pet.Type);
            return query.ToList();
        }

        public List<Pet> GetAllByPrice()
        {
            var list = _petRepo.ReadPets();
            var sortedQuery = list.OrderBy(pet => pet.Price);
            return sortedQuery.ToList();
        }

        public List<Pet> GetFiveCheapestPets()
        {
            var list = _petRepo.ReadPets();
            var sortedQuery = list.OrderBy(pet => pet.Price);
            var fiveCheapest = sortedQuery.Take(5);
            return fiveCheapest.ToList();
        }
        public List<Pet> GetAllByColor(string color)
        {
            var list = _petRepo.ReadPets();
            var query = list.Where(pet => pet.Color.Equals(color));
            query.OrderBy(pet => pet.Color);
            return query.ToList();
        }

        public Pet FindPetById(int id)
        {
            return _petRepo.ReadById(id);
        }

        public Pet UpdatePet(Pet petUpdate)
        {
            var pet = FindPetById(petUpdate.Id);
            pet.Name = petUpdate.Name;
            pet.Type = petUpdate.Type;
            pet.BirthDate = petUpdate.BirthDate;
            pet.SoldDate = petUpdate.SoldDate;
            pet.Color = petUpdate.Color;
            pet.PreviousOwner = petUpdate.PreviousOwner;
            pet.Price = petUpdate.Price;
            return pet;
        }
        public Pet DeletePet(int id)
        {
            return _petRepo.Delete(id);
        }
    }
}
