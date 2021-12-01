using System;
using System.Collections.Generic;

#nullable disable

namespace Wpf_DangKyMonHoc.Models
{
    public partial class MonHocDuocMo
    {
        public int Id { get; set; }
        public int Soluong { get; set; }
        public bool Trangthai { get; set; }
        public string MaCdk { get; set; }
        public string MaMh { get; set; }
        public string NkapDung { get; set; }

    }
}
