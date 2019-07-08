using System.Collections.Generic;
using System.Threading.Tasks;
using WishListLabs.Models;

namespace WishListLabs.Repository
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task Delete(int id);
        Task<IList<T>> GetAll(int pageSize, int page);
        Task<T> GetItem(int id);
        Task InsertItem(T model);
        Task Update(T model);
    }
}