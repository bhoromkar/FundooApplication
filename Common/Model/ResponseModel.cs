using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; } = "Execution Successfully Done";

        public object Data { get; set; }
    }
}
