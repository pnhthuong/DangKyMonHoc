using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Lop
    {
        public Lop()
        {
            SinhViens = new HashSet<SinhVien>();
        }

        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public byte Siso { get; set; }
        public string MaNk { get; set; }
        public string MaNganh { get; set; }

        public virtual Nganh MaNganhNavigation { get; set; }
        public virtual NienKhoa MaNkNavigation { get; set; }
        public virtual ICollection<SinhVien> SinhViens { get; set; }
    }
}
