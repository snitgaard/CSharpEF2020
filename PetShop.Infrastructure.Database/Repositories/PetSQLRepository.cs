using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetShop.Core.DomainServices;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Database.Repositories
{
    public class PetSqlRepository : IPetRepository
    {
        private readonly PetShopContext _ctx;
        public PetSqlRepository(PetShopContext ctx)
        {
            _ctx = ctx;
        }

        public Pet Create(Pet pet)
        {
            var createdPet = _ctx.Pets.Add(pet).Entity;
            _ctx.SaveChanges();
            return createdPet;
        }

        public Pet Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Pet ReadById(int id)
        {
            return _ctx.Pets.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Pet> ReadPets()
        {
            return _ctx.Pets;
        }

        public Pet Update(Pet petUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
