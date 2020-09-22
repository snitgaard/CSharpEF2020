using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Database
{
    public class PetShopContext: DbContext
    {
        public PetShopContext(DbContextOptions<PetShopContext> opt) : base(opt)
        {
            
        }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
    }
}
