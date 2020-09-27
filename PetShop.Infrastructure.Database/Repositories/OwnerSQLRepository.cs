using PetShop.Core.DomainServices;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PetShop.Infrastructure.Database.Repositories
{
    public class OwnerSqlRepository : IOwnerRepository
    {
        private readonly PetShopContext _ctx;
        public OwnerSqlRepository(PetShopContext ctx)
        {
            _ctx = ctx;
        }
        public Owner Create(Owner owner)
        {
            var createdOwner = _ctx.Add(owner).Entity;
            _ctx.SaveChanges();
            return createdOwner;
        }

        public Owner Delete(int id)
        {
            var ownerRemoved = _ctx.Remove(new Owner { Id = id }).Entity;
            _ctx.SaveChanges();
            return ownerRemoved;
        }

        public Owner ReadById(int id)
        {
            return _ctx.Owners.AsTracking().Include(o => o.Pets).FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Owner> ReadOwners()
        {
            return _ctx.Owners;
        }

        public Owner Update(Owner ownerUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
