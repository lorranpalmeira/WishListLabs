
using System.Collections.Generic;
using WishListLabs.Models;

namespace WishListLabs
{
    public class Wishes : BaseModel
    {
        public int userId { get; set; }

        public int IdProduct { get; set; }

        public IList<Product> Products { get; set; }
    }
}