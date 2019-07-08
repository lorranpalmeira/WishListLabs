

using Microsoft.AspNetCore.Mvc;
using WishListLabs.Config;
using WishListLabs.Models;
using WishListLabs.Repository;

namespace WishListLabs.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController<User>
    {
        public UsersController(IBaseRepository<User> baseRepository, IContextMongoDb context)
        : base(baseRepository, context){            
        }
    }
}