using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishListLabs.Models;

namespace WishListLabs.Config
{
    public interface IContextMongoDb
    {

        IMongoCollection<BaseModel> BaseModel { get; }
        IMongoCollection<Product> Product { get; }
        IMongoCollection<User> User { get; }
        IMongoCollection<Wishes> Wishes { get; }

    }
}
