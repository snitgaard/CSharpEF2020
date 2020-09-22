using System;
using System.Collections.Generic;
using System.Text;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationServices
{
    public interface IOwnerService
    {
        Owner NewOwner(string name, string address);

        Owner CreateOwner (Owner owner);
        List<Owner> GetAllOwners();
        Owner FindOwnerById (int id);
        Owner UpdateOwner(Owner updateOwner);
        Owner DeleteOwner (int id);
        List<Owner> GetAllByName(string name);
    }
}
