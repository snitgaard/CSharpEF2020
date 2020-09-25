using System;
using System.Collections.Generic;
using System.Text;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Database
{
    public class DBInitializer
    {
        public static void SeedDB(PetShopContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            var pet1 = ctx.Pets.Add(new Pet()
            {
                Name = "BillyJoel",
                Type = "Dog",
                Color = "Golden",
                BirthDate = new DateTime(2018, 6, 10),
                Price = 100,
                SoldDate = new DateTime(2018, 7, 10),
                PreviousOwner = "JohnnyBravo"
            }).Entity;
            var pet2 = ctx.Pets.Add(new Pet()

            {
                Name = "MichaelJackson",
                Type = "Cat",
                Color = "Black",
                BirthDate = new DateTime(2015, 4, 22),
                Price = 100,
                SoldDate = new DateTime(2020, 6, 1),
                PreviousOwner = "HallAndOates"
            }).Entity;
            var owner1 = ctx.Owners.Add(new Owner()
            {
                Name = "MichaelJackson",
                Address = "NeverLand"
            }).Entity;
            var owner2 = ctx.Owners.Add(new Owner()
            {
                Name = "FogHat",
                Address = "GreatMusicStreet"
            }).Entity;
            ctx.SaveChanges();
        }
    }
}
