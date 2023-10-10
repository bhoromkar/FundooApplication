using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Model
{
    public class LabelModel
    {
        public long LabelId { get; set; }

        public string LabelName { get; set; }

        public long NoteId { get; set; }
    }
}
