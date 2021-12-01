using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class ChucVu
    {
        public ChucVu()
        {
            GiangViens = new HashSet<GiangVien>();
            NhanViens = new HashSet<NhanVien>();
        }

        public string MaChucVu { get; set; }
        public string TenChucVu { get; set; }

        public virtual ICollection<GiangVien> GiangViens { get; set; }
        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
