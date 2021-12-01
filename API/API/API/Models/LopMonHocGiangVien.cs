using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class LopMonHocGiangVien
    {
        public int Id { get; set; }
        public string MaGv { get; set; }
        public string MaLmh { get; set; }

        public virtual GiangVien MaGvNavigation { get; set; }
        public virtual LopMonHoc MaLmhNavigation { get; set; }
    }
}
