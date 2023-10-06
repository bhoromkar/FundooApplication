using Common.Model;
using Microsoft.AspNetCore.Http;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interface
{
    public interface INoteBusiness
    {
        public NoteEntity CreateNote(NoteModel model, long userId);
        public NoteEntity UpdateNote(NoteModel model, long userId, long noteId);
        public IEnumerable<NoteEntity> GetAllNotes(long userId);
        public NoteEntity GetNoteById( long noteId , long userId);
        public bool IsDeleteNote(long noteId, long userId);
       public  bool IsPinNote(long noteId, long userId);
        public string ChangeColor(string newColor, long userId, long noteId);
        public bool IsArchive(long userId, long noteId);

        public bool IsTrash(long userId, long noteId);
        public bool RemindMe(long userId, long noteId);
        public string  UploadImage(IFormFile image, long noteId, long userId);
    }
}
