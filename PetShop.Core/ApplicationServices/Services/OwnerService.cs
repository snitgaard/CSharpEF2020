using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetShop.Core.DomainServices;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationServices.Services
{
    public class OwnerService: IOwnerService
    {
        private readonly IOwnerRepository _ownerRepo;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepo = ownerRepository;
        }

        public Owner NewOwner(string name, string address)
        {
            var owner = new Owner()
            {
                Name = name,
                Address = address
            };
            return owner;
        }

        public Owner CreateOwner(Owner owner)
        {
            return _ownerRepo.Create(owner);
        }

        public List<Owner> GetAllOwners()
        {
            return _ownerRepo.ReadOwners().ToList();
        }
        public Owner FindOwnerById(int id)
        {
            return _ownerRepo.ReadById(id);
        }

        public Owner UpdateOwner (Owner ownerUpdate)
        {
            var owner = FindOwnerById(ownerUpdate.Id);
            owner.Name = ownerUpdate.Name;
            owner.Address = ownerUpdate.Address;
            return owner;
        }
        public Owner DeleteOwner(int id)
        {
            return _ownerRepo.Delete(id);
        }

        public List<Owner> GetAllByName(string name)
        {
            var list = _ownerRepo.ReadOwners();
            var query = list.Where(owner => owner.Name.Equals(name));
            query.OrderBy(owner => owner.Name);
            return query.ToList();
        }
    }
}
