using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WishListLabs.Config;
using WishListLabs.Models;
using WishListLabs.Repository;

namespace WishListLabs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
     public abstract class BaseController<T> : ControllerBase
     where T : BaseModel 
    {

        private readonly IBaseRepository<T> _baseRepository;
        private IContextMongoDb _context;

        public BaseController(IBaseRepository<T> baseRepository, IContextMongoDb context)
        {
           this._baseRepository = baseRepository;
        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get
        (
            [FromQuery(Name = "page_size")] int pageSize,
            [FromQuery(Name = "page")] int page
        )
        {            
            var list = await _baseRepository.GetAll(pageSize, page);
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Post(T model)
        {
           await _baseRepository.InsertItem(model);
           return Ok(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           await _baseRepository.Delete(id);

           return Ok(200);
        }


    }
}