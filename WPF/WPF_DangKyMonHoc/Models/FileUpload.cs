using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wpf_DangKyMonHoc.Models
{
    class FileUpload
    {
        public byte[] hinh { get; set; }
        public string tenhinh { get; set; }
        public string name { get; set; }
    }
}
