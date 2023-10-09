using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace Repository.Entity
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        [ForeignKey("Note")]
        public long NoteId { get; set; }
       
        [ForeignKey("Users")]
        public long UserId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual NoteEntity Note { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual UserEntity User { get; set; }





    }
}
