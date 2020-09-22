using System;
using System.Collections.Generic;
using System.Text;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationServices
{
    public interface IPetService
    {
        Pet NewPet(string name, string type, DateTime birthDate, DateTime soldDate, string color,
            string previousOwner, double price);

        Pet CreatePet(Pet pet);
        List<Pet> GetPets();
        List<Pet> GetAllByType(string type);
        List<Pet> GetAllByPrice();
        List<Pet> GetFiveCheapestPets();
        List<Pet> GetAllByColor(string color);
        Pet FindPetById(int id);
        Pet UpdatePet(Pet updatePet);
        Pet DeletePet(int id);
    }
}
