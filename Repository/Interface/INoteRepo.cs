using Common.Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Repository.Interface
{
    public interface INoteRepo
    {
        public NoteEntity CreateNote(NoteModel model, long userId);
        public NoteEntity UpdateNote(NoteModel model, long userId, long noteId);
        public IEnumerable<NoteEntity> GetAllNotes(long userId);

        public NoteEntity GetNoteById(long noteId, long userId);
        public bool IsDeleteNote(long noteId, long userId);
       public  bool IsPin(long noteId, long userId);
        public string ChangeColor(string newColor, long userId, long noteId);
        public bool IsArchive(long userId, long noteId);
        public bool IsTrash(long userId, long noteId);
        public bool RemindMe(long userId, long noteId);
//public string UploadImage(FileUploadModel fileUploadModel, long noteId, long userId);
        public string UploadImage(IFormFile image, long noteId, long userId);
       public IEnumerable<NoteEntity> Search( string data, long userId);
       public NoteEntity CreateCopy(long NoteId, long UserId);

    }
}
