using System;
using System.Collections.Generic;
using System.Text;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationServices
{
    public interface IPetTypeService
    {
        PetType NewPetType(string type);

        PetType CreatePetType(PetType petType);
        List<PetType> GetPetTypes();
        PetType FindPetTypeById(int id);
        PetType UpdatePetType(PetType updatePetType);
        PetType DeletePetType(int id);
        List<PetType> GetAllByType(string type);
    }
}
