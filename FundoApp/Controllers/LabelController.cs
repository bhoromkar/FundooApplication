using System.Collections.Generic;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using Business.Service;
using Common.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Repository.Context;
using Repository.Entity;

namespace FundoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : Controller
    {
        public readonly ILabelBusiness _labelBusiness;
        public readonly IDistributedCache _distributedCache;
        public readonly FundoDBContext _userDBContext;


        public LabelController(ILabelBusiness labelBusiness , IDistributedCache distributedCache,FundoDBContext userDBContext)
        {
            this._labelBusiness = labelBusiness;
           this._distributedCache= distributedCache;
            this._userDBContext = userDBContext;
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(LabelModel labelModel)
        {


            var userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _labelBusiness.Create(labelModel, userId);
            if (result != null)
            {
                {
                    return Ok(new { success = true, Message = "Label Created", result });
                }
            }

            return NotFound(new { success = false, Message = "Label Not Created" });

        }
        [HttpPut]
        [Route("update")]
        public IActionResult Update(LabelModel labelModel)
        {


            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _labelBusiness.Update(labelModel, userId);
            if(result == null) return NotFound(new { success = false, Message = "Label Not Updated" });
            {
                {
                    return Ok(new { success = true, Message = "Label Updated", result });
                }
            }

            //return NotFound(new { success = false, Message = "Label Not Updated" });

        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {


            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _labelBusiness.GetAll(userId);
            if (result != null)
            {
                {
                    return Ok(new { success = true, Message = "Label Created", result });
                }
            }

            return NotFound(new { success = false, Message = "Label Not Created" });

        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(long labelId)
        {


            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _labelBusiness.Delete(userId, labelId);
            if (result!=false)
            {
                {
                    return Ok(new { success = true, Message = "Label Deleted", });
                }
            }

            return NotFound(new { success = false, Message = "Label Not Deleted" });

        }


        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            long userId = long.Parse(User.FindFirst("UserID").Value);

            var cacheKey = "LabelList";
            string serializedLabelList;
            var LabelList = new List<LabelEntity>();
            var redisLabelList = await _distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                LabelList = _userDBContext.Label.ToList();
                serializedLabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);

                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await _distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }

    }
}
