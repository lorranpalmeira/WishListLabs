using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using WishListLabs.Models;

namespace WishListLabs.Config
{
    public class ContextMongoDb : IContextMongoDb
    {

        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static bool IsSSL { get; set; }

        private IMongoDatabase _database { get; }

        public ContextMongoDb()
        {

            try
            {
                MongoClientSettings setting = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if (IsSSL)
                {
                    setting.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }

                var mongoClient = new MongoClient(setting);
                _database = mongoClient.GetDatabase(DatabaseName);


            }
            catch
            {

                throw new Exception("It´s not possible to connect.");
            }
        }


        // Config Tables

        public IMongoCollection<BaseModel> BaseModel 
        {
            get 
            {
                return _database.GetCollection<BaseModel>("BaseModel");
            }
        }

        public IMongoCollection<Product> Product
        {
            get
            {
                return _database.GetCollection<Product>("Product");
            }
        }

        public IMongoCollection<User> User
        {
            get
            {
                return _database.GetCollection<User>("User");
            }
        }

        public IMongoCollection<Wishes> Wishes
        {
            get
            {
                return _database.GetCollection<Wishes>("Wishes");
            }
        }

        
    }
}
