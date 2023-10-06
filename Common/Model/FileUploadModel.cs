using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Model
{
    public class FileUploadModel
    {
        public IFormFile files { get; set; }
    }
}
