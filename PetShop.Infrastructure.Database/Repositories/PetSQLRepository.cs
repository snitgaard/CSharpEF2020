using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.EntityFrameworkCore;
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
            _ctx.Attach(pet).State = EntityState.Added;
            _ctx.SaveChanges();
            return pet;
        }

        public Pet Delete(int id)
        {
            var petRemoved = _ctx.Remove(new Pet {Id = id}).Entity;
            _ctx.SaveChanges();
            return petRemoved;
        }

        public Pet ReadById(int id)
        {
            return _ctx.Pets.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Pet> ReadPets()
        {
            return _ctx.Pets.AsNoTracking().Include(p => p.Owner);
        }

        public Pet Update(Pet petUpdate)
        {
            _ctx.Attach(petUpdate).State = EntityState.Modified;
            //_ctx.Entry(petUpdate).Reference(p => p.Name).IsModified = true;
            _ctx.SaveChanges();
            return petUpdate;
        }
    }
}
