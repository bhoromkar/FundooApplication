using Business.Interface;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Authorization;

namespace FundoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : Controller
    {
        public readonly INoteBusiness noteBusiness;

        public NoteController(INoteBusiness noteBusiness)
        {
            this.noteBusiness = noteBusiness;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateNote(NoteModel noteModel)
        {
            long userId = long.Parse(User.FindFirst("UserID").Value);
            var result = noteBusiness.CreateNote(noteModel, userId);
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
            var result = noteBusiness.UpdateNote(noteModel, userId, noteId);
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
            var result = noteBusiness.GetNoteById(noteId, userId);
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
            var result = noteBusiness.GetAllNotes(userId);
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
            var result = noteBusiness.IsDeleteNote(noteId, userId);
            if (result != true)
            {
                return Ok(new { success = true, Message = "Note Deleted" });
            }

            return BadRequest(new { success = false, Message = "Failed to Delete" });
        }



        //[HttpGet]


            //[Route("GetByUserId")]
            //public IActionResult GetNoteByUserId(long noteId)
            //{
            //    long userId = long.Parse(User.FindFirst("UserID").Value);

            //    var result = noteBusiness.GetNoteById(noteId, userId);
            //    if (result != null)
            //    {
            //        return Ok(new { success = true, Message = "Data Retrieved ", result });
            //    }

            //    return BadRequest(new { success = false, Message = "Data Retrieve failed" });
            //}
        }
    }