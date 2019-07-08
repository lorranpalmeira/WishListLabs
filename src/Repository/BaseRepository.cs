using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishListLabs.Config;
using WishListLabs.Models;

namespace WishListLabs.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly IMongoCollection<T> _mongoCollection;


        public BaseRepository(IMongoCollection<T> mongoCollection)
        {
            this._mongoCollection = mongoCollection;
        }

        public async Task<IList<T>> GetAll(int pageSize, int page)
        {
            try
            {
                var sizeList = await _mongoCollection.CountDocumentsAsync(x => true);

                if (sizeList == 0)
                    return new List<T>();

                if (page == 0 || pageSize == 0)
                    pageSize = (int)sizeList;

                var size = sizeList / pageSize;

                if (page > size || pageSize > sizeList)
                    throw new Exception(
                        $"A página passada é maior que a quantidade existente, Página Máxima : {size}" +
                        $" ou ajuste para um PageSize Menor.");

                var entityAsync = _mongoCollection.Find(m => true).Skip((page * pageSize) - pageSize).Limit(pageSize);

                var result = await entityAsync.ToListAsync();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> GetItem(int id)
        {
            try
            {
                var entityAsync = await _mongoCollection.FindAsync(x => x.Id == id);

                var result = await entityAsync.FirstOrDefaultAsync();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task InsertItem(T model)
        {
            try
            {
                model.Id = 0;

                var retCode = _mongoCollection.Find(m => true)
                    .SortByDescending(x => x.Id)
                    .Project(u => u.Id)
                    .FirstOrDefault();

                retCode = retCode == 0 ? 1 : retCode + 1;

                model.Id = retCode;

                await _mongoCollection.InsertOneAsync(model);
            }
            catch
            {
                throw;
            }

        }

        public async Task Update(T model)
        {
            try
            {
                await _mongoCollection.ReplaceOneAsync(x => x.Id == model.Id, model);
            }
            catch
            {
                throw;
            }

        }

        public async Task Delete(int id)
        {
            try
            {
                var exists = await _mongoCollection.FindAsync(x => x.Id == id);
                var existItem = await exists.AnyAsync();

                if (existItem)
                    await _mongoCollection.DeleteOneAsync(x => x.Id == id);
                else
                    throw new Exception("The item Doesn´t exist.");
            }
            catch
            {
                throw;
            }

        }


    }
}