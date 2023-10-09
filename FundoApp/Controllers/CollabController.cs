using Business.Interface;
using Business.Service;
using Common.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FundoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollabController : Controller
    {
        public readonly ICollabBusiness _collabBusiness;

        public CollabController(ICollabBusiness collabBusiness)
        {
            this._collabBusiness = collabBusiness;

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

    }
    }
                    
                   
        


    


