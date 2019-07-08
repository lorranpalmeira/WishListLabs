using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WishListLabs.Models;

namespace WishListLabs.Repository
{
    public class UserRepository : BaseRepository<User>
    {

        private readonly IMongoCollection<User> _userCollection;
        public UserRepository(IMongoCollection<User> userCollection) : base(userCollection)
        {
            this._userCollection = userCollection;
        }
    }
}
