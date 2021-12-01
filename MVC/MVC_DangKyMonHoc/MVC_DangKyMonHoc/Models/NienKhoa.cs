using System;
using System.Collections.Generic;

#nullable disable

namespace MVC_DangKyMonHoc.Models
{
    public partial class NienKhoa
    {
        public NienKhoa()
        {
            Lops = new HashSet<Lop>();

        }

        public string MaNk { get; set; }
        public string TenNk { get; set; }
        public string MaCtdt { get; set; }


        public virtual ICollection<Lop> Lops { get; set; }
    }
}
