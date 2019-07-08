
using Microsoft.AspNetCore.Mvc;
using WishListLabs.Config;
using WishListLabs.Controllers;
using WishListLabs.Models;
using WishListLabs.Repository;

namespace WishListLabs
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController<Product> 
    {
        public ProductsController(IBaseRepository<Product> baseRepository, IContextMongoDb context)
        : base(baseRepository, context)
        {                       
        }

    }
}