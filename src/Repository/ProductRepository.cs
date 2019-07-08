using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishListLabs.Models;

namespace WishListLabs.Repository
{
    public class ProductRepository : BaseRepository<Product>
    {
        private readonly IMongoCollection<Product> _productCollection;
        public ProductRepository(IMongoCollection<Product> productCollection ) : base(productCollection)
        {
            this._productCollection = productCollection;
        }
    }
}
