using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishListLabs.Models;

namespace WishListLabs.Repository
{
    public class WishesRepository : BaseRepository<Wishes>, IWishesRepository
    {
        private readonly IMongoCollection<Wishes> _wishesCollection;

        private readonly IMongoCollection<Product> _productRepository;

        public WishesRepository(
            IMongoCollection<Wishes> wishesCollection,
            IMongoCollection<Product> productRepository ) : base(wishesCollection)
        {
            this._wishesCollection = wishesCollection;
            this._productRepository = productRepository;
        }

         public async Task InsertItem(IList<Wishes> wishList, int userId)
         {
            try
            {
                    Wishes wish = new Wishes();
                    wish.Id = 0;
                    wish.userId = userId;
                    wish.Products = new List<Product>();

                    var retCode = _wishesCollection.Find(m => true)
                        .SortByDescending(x => x.Id)
                        .Project(u => u.Id)
                        .FirstOrDefault();

                    retCode = retCode == 0 ? 1 : retCode + 1;

                    wish.Id = retCode;

                foreach (var item in wishList)
                {
                    var prod = await _productRepository.FindAsync(x => x.Id == item.IdProduct);
                    
                    var prodItem = await prod.FirstOrDefaultAsync();

                    if (prodItem.Id > 0)
                        wish.Products.Add(prodItem);
                    else
                        throw new Exception($"Id {item.IdProduct} não exite.");
                }
                await _wishesCollection.InsertOneAsync(wish);
            }
            catch
            {
                throw;
            }

        }

        public async Task Delete( int userId, int productId)
        {
            try
            {
                await _wishesCollection.FindOneAndDeleteAsync(x => x.userId == userId );
            }
            catch
            {
                throw;
            }
        }
        


        public async Task<Wishes> GetItem( int userId)
        {
            try
            {
                var foundWish = await _wishesCollection.FindAsync(x => x.userId == userId );
                var firstWish = await foundWish.FirstOrDefaultAsync();

                return firstWish;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateItem(int userId, int productId)
        {
            try
            {
                var item = await _wishesCollection.FindAsync(x => x.userId == userId && x.IdProduct == productId);
                var firstItem = await item.FirstOrDefaultAsync();

                var update = await _wishesCollection.ReplaceOneAsync(x => x.IdProduct == productId ,firstItem);
            }
            catch
            {
                throw;
            }
        }

    }
}
