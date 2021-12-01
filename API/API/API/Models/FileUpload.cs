using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class FileUpload
    {
        public byte[] hinh { get; set; }
        public string tenhinh { get; set; }
        public string name { get; set; }// "SinhVien,GiangVien,NhanVien"
    }
}
