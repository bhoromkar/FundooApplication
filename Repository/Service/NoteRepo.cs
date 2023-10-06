using Common.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Repository.Context;
using Repository.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Repository.Service
{
    public class NoteRepo : INoteRepo
    {
        public readonly FundoDBContext _userDBContext;
        public readonly IConfiguration Iconfiguration;
        public static IWebHostEnvironment _weHostEnvironment;


        public NoteRepo(FundoDBContext userDBContext, IConfiguration configuration, IWebHostEnvironment weHostEnvironment)
        {
            this.Iconfiguration = configuration;
            this._userDBContext = userDBContext;
            _weHostEnvironment = weHostEnvironment;
        }

        public NoteEntity CreateNote(NoteModel model, long userId)
        {
            try
            {
                var result = _userDBContext.Users.First(x => x.userId == userId);

                NoteEntity noteEntity = new NoteEntity();
                if (result != null)
                {
                    noteEntity.UserId = userId;
                    noteEntity.NoteTitle = model.NoteTitle;
                    noteEntity.NoteDescription = model.NoteDescription;
                    noteEntity.Reminder = model.Reminder;
                    noteEntity.Image = model.Image;
                    //noteEntity.IsArchive = false;
                    //noteEntity.IsPin = false;
                    //noteEntity.IsTrash = false;
                    noteEntity.Color = model.Color;
                    noteEntity.CreatedTime = DateTime.Now;
                    _userDBContext.Note.Add(noteEntity);
                    _userDBContext.SaveChanges();
                    return noteEntity;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        /// <summary>
        /// Gets the user information for the specified user account and account name. 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> Returns all the note of specified user </returns>
        /// <exception cref="Exception"></exception>

        public IEnumerable<NoteEntity> GetAllNotes(long userId)
        {
            try
            {
                var result = _userDBContext.Users.First(x => x.userId == userId);
                if (result != null)
                {
                    return _userDBContext.Note.Where(x => x.UserId == userId).ToList();
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public NoteEntity GetNoteById(long noteId, long userId)
        {
            try
            {
                var result = _userDBContext.Users.First(x => x.userId == userId);
                if (result != null)
                {
                    return _userDBContext.Note.FirstOrDefault(x => x.NoteId == noteId);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool IsDeleteNote(long noteId, long userId)
        {

            try
            {
                var result = _userDBContext.Users.First(x => x.userId == userId);
                if (result != null)
                {
                    var note = _userDBContext.Note.FirstOrDefault(x => x.NoteId == noteId);
                    if (note != null)
                    {

                        _userDBContext.Note.Remove(note);
                        _userDBContext.SaveChanges();
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public NoteEntity UpdateNote(NoteModel model, long userId, long noteId)
        {
            try
            {
                var noteEntity = _userDBContext.Note.FirstOrDefault(e => e.NoteId == noteId);
                if (noteEntity != null)
                {
                    noteEntity.NoteTitle = model.NoteTitle;
                    noteEntity.NoteDescription = model.NoteDescription;
                    noteEntity.Reminder = model.Reminder;
                    noteEntity.Image = model.Image;
                    //noteEntity.IsArchive = false;
                    //noteEntity.IsPin = false;
                    //noteEntity.IsTrash = false;
                    noteEntity.Color = model.Color;
                    noteEntity.CreatedTime = DateTime.Now;
                    _userDBContext.Note.Update(noteEntity);
                    _userDBContext.SaveChanges();
                    return noteEntity;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //implementation of Pin Note Method
        /// <summary>
        /// to check  note is pinned or not 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns> if pinned returns true else false</returns>
        public bool IsPin(long userId, long noteId)
        {
            try
            {
                var result = _userDBContext.Users.First(x => x.userId == userId);
                if (result != null)
                {
                    var note = _userDBContext.Note.First(x => x.NoteId == noteId);
                    if (note.IsPin == false)
                    {
                        return note.IsPin == true;
                    }
                }

                return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsArchive(long userId, long noteId)
        {
            try
            {
                var result = _userDBContext.Users.First(x => x.userId == userId);
                if (result != null)
                {
                    var note = _userDBContext.Note.First(x => x.NoteId == noteId);
                    if (note.IsArchive == false)
                    {
                        return true;
                    }


                }

                return false;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool RemindMe(long userId, long noteId)
        {
            try
            {
                var result = _userDBContext.Users.First(x => x.userId == userId);
                if (result != null)
                {
                    var note = _userDBContext.Note.First(x => x.NoteId == noteId);
                    if (note != null)
                    {
                        if (note.Reminder == DateTime.Now)
                        {
                            return true;
                        }

                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsTrash(long userId, long noteId)
        {
            try
            {
                var result = _userDBContext.Users.First(x => x.userId == userId);
                if (result != null)
                {
                    var note = _userDBContext.Note.First(x => x.NoteId == noteId);
                    if (note.IsTrash == false)
                    {
                        return true;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string ChangeColor(string newColor, long userId, long noteId)
        {
            try
            {

                var result = _userDBContext.Users.First(x => x.userId == userId);
                var note = _userDBContext.Note.First(x => x.NoteId == noteId);
                if (result != null && note != null)
                {
                    note.Color = newColor;
                    _userDBContext.Note.Update(note);
                    _userDBContext.SaveChanges();
                    return note.Color;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string UploadImage(IFormFile image, long noteId, long userId)
        {
            try
            { var note = _userDBContext.Note.First( x => x.NoteId == noteId);
                if (note != null)
                {


                    if (image.Length > 0)
                    {
                        string path = _weHostEnvironment.WebRootPath + "\\Upload\\";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        using (FileStream fs = File.Create(path + image.FileName))
                        {
                           // image.FileName.CopyTo(fs);
                            note.Image = image.FileName;
                            _userDBContext.Note.Update(note);
                            _userDBContext.SaveChanges();
                            fs.Flush();
                            
                            return "Uploaded successfully.";

                            
                        }

                    }
                    
                }
                return null;

            
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }
    }
}


