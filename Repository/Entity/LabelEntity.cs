using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Repository.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long LabelId { get; set; }
        
        public string LabelName { get; set; }
        [ForeignKey("Users")]
        public long UserId { get; set; }

        [ForeignKey("Note")]
        public long NoteId { get; set; }

        [JsonIgnore]
        public virtual NoteEntity Note { get; set; }
      
        [JsonIgnore]
        public virtual UserEntity User { get; set; }
    }
}
