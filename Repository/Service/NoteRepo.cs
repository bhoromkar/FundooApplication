using Common.Model;
using Microsoft.Extensions.Configuration;
using Repository.Context;
using Repository.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Service
{
    public class NoteRepo : INoteRepo
    {
        public readonly FundoDBContext _userDBContext;
        public readonly IConfiguration Iconfiguration;


        public NoteRepo(FundoDBContext userDBContext, IConfiguration configuration)
        {
            this.Iconfiguration = configuration;
            this._userDBContext = userDBContext;
        }
        public NoteEntity CreateNote(NoteModel model, long userId)
        {  try
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
    }
}
