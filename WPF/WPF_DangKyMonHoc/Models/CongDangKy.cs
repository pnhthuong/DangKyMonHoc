using System;
using System.Collections.Generic;

#nullable disable

namespace Wpf_DangKyMonHoc.Models
{
    public partial class CongDangKy
    {
       

        public string MaCdk { get; set; }
        public string TenCdk { get; set; }
        public DateTime? ThoigianBatDau { get; set; }
        public DateTime? ThoigianKetThuc { get; set; }
        public bool Trangthai { get; set; }
        public string MaHk { get; set; }
        public string MaNh { get; set; }

        
    }
}
