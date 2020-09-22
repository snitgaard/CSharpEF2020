using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.Entity
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pet> OwnerPetList { get; set; }
        public string Address { get; set; }
    }
}
