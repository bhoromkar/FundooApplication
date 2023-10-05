using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Model
{
    public class NoteModel
    {   
        public string NoteTitle { get; set; }
        public string NoteDescription { get; set; }
        public string Color { get; set; }
        public string Image { get; set; } 
        public DateTime Reminder { get; set; }
        public bool IsPin { get; set; } = false;
        public bool IsTrash { get; set; }=false;
        public bool IsArchive { get; set; } = false;



    }
} 
