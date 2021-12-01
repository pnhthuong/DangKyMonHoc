using System;
using System.Collections.Generic;

#nullable disable

namespace MVC_DangKyMonHoc.Models
{
    public partial class LopMonHoc
    {
        public LopMonHoc()
        {
           
        }

        public string MaLmh { get; set; }
        public string TenLmh { get; set; }
        public int Siso { get; set; }
        public string MaCdk { get; set; }
        public string MaMh { get; set; }

        public virtual CongDangKy MaCdkNavigation { get; set; }
        public virtual MonHoc MaMhNavigation { get; set; }
       
    }
}
