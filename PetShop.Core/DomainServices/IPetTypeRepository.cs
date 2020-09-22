using System;
using System.Collections.Generic;
using System.Text;
using PetShop.Core.Entity;

namespace PetShop.Core.DomainServices
{
    public interface IPetTypeRepository
    {
        PetType Create(PetType petType);
        PetType ReadById(int id);
        IEnumerable<PetType> ReadPetTypes();
        PetType Update(PetType petTypeUpdate);
        PetType Delete(int id);
    }
}
