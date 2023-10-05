using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Repository.Entity
{
    public class NoteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId { get; set; }
        public string NoteTitle { get; set; }
        public string NoteDescription { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool IsPin { get; set; } 
        public bool IsTrash { get; set; }
        public bool IsArchive { get; set; }
        
        public DateTime Reminder { get; set; }  
        public DateTime CreatedTime { get; set; } 

        [ForeignKey("Users")]
        public long UserId { get; set; }

    }
}
