using System;
using System.Collections.Generic;
using System.Text;
using PetShop.Core.DomainServices;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Data
{
    public class OwnerRepository : IOwnerRepository
    {
        static int id = 1;
        static List<Owner> _owners = new List<Owner>();

        public Owner Create(Owner owner)
        {
            owner.Id = id++;
            _owners.Add(owner);
            return owner;
        }
        public Owner ReadById(int id)
        {
            foreach (var owner in _owners)
            {
                if (owner.Id == id)
                {
                    return owner;
                }
            }
            return null;
        }

        public IEnumerable<Owner> ReadOwners()
        {
            return _owners;
        }
        public Owner Update(Owner ownerUpdate)
        {
            var ownerDB = this.ReadById(ownerUpdate.Id);
            if (ownerDB != null)
            {
                ownerDB.Name = ownerUpdate.Name;
                ownerDB.Address = ownerUpdate.Address;
            }
            return null;
        }

        public Owner Delete(int id)
        {
            var ownerFound = this.ReadById(id);
            if (ownerFound != null)
            {
                _owners.Remove(ownerFound);
                return ownerFound;
            }
            return null;
        }
    }
}
