using Business.Interface;
using Common.Model;
using Repository.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Service
{
    public class NoteBusiness : INoteBusiness
    {
        public readonly INoteRepo noteRepo;
        public NoteBusiness(INoteRepo noteRepo)
        {
            this.noteRepo = noteRepo;
        }


        public NoteEntity CreateNote(NoteModel model, long userId)
        {
            try
            {
                return noteRepo.CreateNote(model, userId);
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
                return noteRepo.GetNoteById(noteId, userId);
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
                return noteRepo.UpdateNote(model, userId, noteId);
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<NoteEntity> GetAllNotes( long userId)
        {
            try
            {
                return noteRepo.GetAllNotes(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Get all notes for a given user and note id  from the repository and
        /// return  the notes  for that user and note id in the repository for that note id in the repository         
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="noteId"></param>
        /// <returns>  </returns>
        /// <exception cref="Exception"> </exception>

        public bool IsDeleteNote( long noteId, long userId)
        {
            try
            {
                return  noteRepo.IsDeleteNote( noteId,userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

