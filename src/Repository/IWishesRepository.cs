using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace WishListLabs.Repository
{
    public interface IWishesRepository
    {
        Task Delete(int userId, int productId);

        Task<Wishes> GetItem(int userId);

        Task UpdateItem(int userId, int productId);

        Task InsertItem(IList<Wishes> wishList, int userId);
    }
}