using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastructure.Database
{
    public interface IDBInitializer
    {
        void SeedDB(PetShopContext ctx);
    }
}
