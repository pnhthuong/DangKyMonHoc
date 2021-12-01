using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_DangKyMonHoc.Models
{
    public class FileUpload
    {
        public IFormFile files { get; set; }
        public string name { get; set; }// "SinhVien,GiangVien,NhanVien"
    }
}
