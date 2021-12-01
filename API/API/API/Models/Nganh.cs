using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Nganh
    {
        public Nganh()
        {
            ChuongTrinhDaoTaos = new HashSet<ChuongTrinhDaoTao>();
            Lops = new HashSet<Lop>();
        }

        public string MaNganh { get; set; }
        public string TenNganh { get; set; }
        public string MaKhoa { get; set; }

        public virtual Khoa MaKhoaNavigation { get; set; }
        public virtual ICollection<ChuongTrinhDaoTao> ChuongTrinhDaoTaos { get; set; }
        public virtual ICollection<Lop> Lops { get; set; }
    }
}
