using Common.Model;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface INoteRepo
    {
        public NoteEntity CreateNote(NoteModel model, long userId);
        public NoteEntity UpdateNote(NoteModel model, long userId, long noteId);
        public IEnumerable<NoteEntity> GetAllNotes(long userId);

        public NoteEntity GetNoteById(long noteId, long userId);
        public bool IsDeleteNote(long noteId, long userId);


    }
}
