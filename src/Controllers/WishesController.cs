using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WishListLabs.Config;
using WishListLabs.Models;
using WishListLabs.Repository;

namespace WishListLabs.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WishesController : ControllerBase
    {

        private readonly IContextMongoDb _context;
        private readonly IBaseRepository<Wishes> _baseRepository;
        private readonly IWishesRepository _wishesRepository;

        public WishesController(
            IBaseRepository<Wishes> baseRepository, 
            IWishesRepository wishesRepository,
            IContextMongoDb context
        )
        {
            this._baseRepository = baseRepository;
            this._wishesRepository = wishesRepository;
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get
        (
            [FromQuery(Name = "page_size")] int pageSize,
            [FromQuery(Name = "page")] int page
        )
        {
            var list = await _baseRepository.GetAll(pageSize, page);
            
            var listWishes = new List<Product>();
            foreach (var item in list)
            {
                listWishes.AddRange(item.Products);                
            }           
            return Ok(listWishes);
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var item = await _wishesRepository.GetItem(userId);
            return Ok(item);
        }

        
        [HttpPost("{userId}")]
        public async Task<IActionResult> Post([FromBody]IList<Wishes> wishes, int userId)
        {
            try
            {                
                await _wishesRepository.InsertItem(wishes, userId);
                return Ok(201);
            }
            catch
            {
                return BadRequest("Error contract");
            }
        }

        
        [HttpPut("{userId}/{productId}")]
        public async Task<IActionResult> Put(int userId, int productId)
        {
            await _wishesRepository.UpdateItem(userId,productId );

            return Ok(202);
        }

        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> Delete(int userId, int productId)
        {
            try
            {
                await _wishesRepository.Delete( userId, productId);
            }
            catch
            {
                throw;
            }

            return Ok(200);
        }
    }
}