using System.Security.Cryptography.X509Certificates;
using Business.Interface;
using Business.Service;
using Common.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : Controller
    {
        public readonly ILabelBusiness _labelBusiness;

        public LabelController(ILabelBusiness labelBusiness)
        {
            this._labelBusiness = labelBusiness;
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult Create(LabelModel labelModel)
        {


            long userId = long.Parse(User.FindFirst("UserID").Value);
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
            if (result != null)
            {
                {
                    return Ok(new { success = true, Message = "Label Updated", result });
                }
            }

            return NotFound(new { success = false, Message = "Label Not Updated" });

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
    }
}
