using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Khoa
    {
        public Khoa()
        {
            GiangViens = new HashSet<GiangVien>();
            Nganhs = new HashSet<Nganh>();
        }

        public string MaKhoa { get; set; }
        public string TenKhoa { get; set; }
        public string TenVietTat { get; set; }

        public virtual ICollection<GiangVien> GiangViens { get; set; }
        public virtual ICollection<Nganh> Nganhs { get; set; }
    }
}
