using Business.Interface;
using Business.Service;
using Common.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Repository.Context;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repository.Entity;

namespace FundoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollabController : Controller
    {
        public readonly ICollabBusiness _collabBusiness;
        private readonly IDistributedCache distributedCache;
        public readonly  FundoDBContext _UserdDbContext;



        public CollabController(ICollabBusiness collabBusiness, IDistributedCache distributedCache, FundoDBContext userdDbContext)
        {
            this._collabBusiness = collabBusiness;
            this.distributedCache = distributedCache;
           this._UserdDbContext = userdDbContext;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(CollabModel collabModel)
        {
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _collabBusiness.Create(collabModel, userId);
            if (result != null)
            {
                return Ok(new { success = true, Message = "Collab Created", result });
            }

            return NotFound(new { success = false, Message = "Collab Not Created" });
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            // long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _collabBusiness.GetAll(userId);
            if (result != null)
            {
                return Ok(new
                {
                    success = true,
                    Message = "List of  all collab",
                    result
                });
            }

            return NotFound(new
                {
                    success = false,
                    Message = "No Record Found",
                });
            }

        [HttpGet]
        [Route("GetByID")]
        public IActionResult GetById(long collabId)


        {
           // long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
           // long userId = long.Parse(User.FindFirst("UserID").Value);

            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _collabBusiness.GetById(collabId, userId);
            if (result != null)
            {
                return Ok(new
                {
                    success = true,
                    Message = "Collab Found",
                    result
                });
            }

            return NotFound(new
            {
                success = false,
                Message = "No Record Found",
            });
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(long collabId)

        {
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _collabBusiness.Delete(collabId, userId);
            if (result ==true)
            {
                return Ok(new
                {
                    success = true,
                    Message = "Collab Deleted",
                    result
                });
            }

            return NotFound(new
            {
                success = false,
                Message = "No Record Found",
            });
        }
        [Authorize]
        [HttpGet]
        [Route("rediscollab")]
        //Redis Implementation 
        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            long userId = long.Parse(User.FindFirst("UserID").Value);

            var cacheKey = "CollabList";
            string serializedCollabList;
            var CollabList = new List<CollabEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                CollabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
            }
            else
            {
                CollabList = _UserdDbContext.Collab.ToList();
                serializedCollabList = JsonConvert.SerializeObject(CollabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);

                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(CollabList);
        }


    }
}
                    
                   
        


    


