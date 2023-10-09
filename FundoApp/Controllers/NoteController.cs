using Business.Interface;
using Microsoft.AspNetCore.Http;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Security.Claims;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace FundoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : Controller
    {
        public readonly INoteBusiness _noteBusiness;
        public static IWebHostEnvironment _webHostEnvironment;

        public NoteController(INoteBusiness noteBusiness, IWebHostEnvironment webHostEnvironment)
        {
            this._noteBusiness = noteBusiness;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateNote(NoteModel noteModel)
        {
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _noteBusiness.CreateNote(noteModel, userId);
            if (result != null)
            {
                return Ok(new { success = true, Message = "Note Added Succesfully", result });
            }

            return BadRequest(new { success = false, Message = "Invalid Data!" });

        }

        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNote(NoteModel noteModel, long noteId)

        {
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _noteBusiness.UpdateNote(noteModel, userId, noteId);
            if (result != null)
            {
                return Ok(new { success = true, Message = "Note Updated", result });
            }

            return BadRequest(new { success = false, Message = "Update Failed!" });
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult GetNoteById(long noteId)
        {
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _noteBusiness.GetNoteById(noteId, userId);
            if (result != null)
            {
                return Ok(new { success = true, Message = "Data Retrieved", result });
            }

            return BadRequest(new { success = false, Message = "Data Retrieve Failed!" });
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllNote()
        {
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _noteBusiness.GetAllNotes(userId);
            if (result != null)
            {
                return Ok(new { success = true, Message = "Data Retrieved", result });
            }

            return BadRequest(new { success = false, Message = "Data Retrieve failed! " });
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(long noteId)
        {
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _noteBusiness.IsDeleteNote(noteId, userId);
            if (result != true)
            {
                return Ok(new { success = true, Message = "Note Deleted" });
            }

            return BadRequest(new { success = false, Message = "Failed to Delete" });
        }

        [HttpPatch]
        [Route("Pin")]
        public IActionResult IsPin(long noteId)
        {
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _noteBusiness.IsPinNote(noteId, userId);
            if (result == true)
            {
                return Ok(new { success = true, Message = "Note Pinned" });
            }
            return BadRequest(new { success = false, Message = "Failed to Pin" });
        }
        [HttpPatch]
        [Route("Archieve")]
        public IActionResult IsArchive(long noteId)
        {

            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _noteBusiness.IsArchive(noteId, userId);
            if (result == true)
            {
                return Ok(new { success = true, Message = "Note Archieved" });
            }
            return BadRequest(new { success = false, Message = "Failed to Archieve" });
        }

        [HttpPatch]
        [Route("ChangeColor")]
        public IActionResult ChangeColor(string newColor, long noteId)
        {
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _noteBusiness.ChangeColor(newColor, noteId, userId);
            if (result != null)
            {
                return Ok(new { success = true, Message = "Note Color Changed" });
            }
            return BadRequest(new { success = false, Message = "Failed to Change Color" });
        }
        [HttpPatch]
        [Route("Trash")]
        public IActionResult IsTrash(long noteId)
        {
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _noteBusiness.IsTrash(noteId, userId);
            if (result == true)
            {
                return Ok(new { success = true, Message = "Note Trashed" });
            }
            return BadRequest(new { success = false, Message = "Failed to Trash" });
        }
        [HttpPatch]
        [Route("Reminder")]
        public IActionResult Reminder(long noteId)
        {
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _noteBusiness.RemindMe(noteId, userId);
            if (result == true)
            {
                return Ok(new { success = true, Message = "Note Reminder" });
            }
            return BadRequest(new { success = false, Message = "Failed to Reminder" });
        }

        [HttpPatch]
        [Route("UploadImage")]
        public IActionResult UploadImage(long noteId, IFormFile image)
        {
            //string fileContents = Convert(fileUploadModel.files);
            //var jsonObject = JsonSerializer.Deserialize<FileUploadModel>(fileContents);

            //byte[] fileContent = Convert.FromBase64String(fileUploadModel.files);

            // var noteId = 1;
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _noteBusiness.UploadImage(image, noteId, userId);
            if (result != null)
            {
                return Ok(new { success = true, Message = "Image Uploaded" });
            }
            return BadRequest(new { success = false, Message = "Image Not Uploaded" });
        }

        [HttpGet]
        [Route("Search")]
        public IActionResult search( string data)
        {
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = _noteBusiness.Search( data, userId);
            if (result != null)
            {
                return Ok(new { success = true, Message = "Data Retrieved", result });
            }
            return NotFound(new { success = false, Message = "Data  not Retrieved" });
        }
    }
}


           
        
    