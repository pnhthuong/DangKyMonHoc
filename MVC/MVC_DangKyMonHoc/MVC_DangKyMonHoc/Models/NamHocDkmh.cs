using System;
using System.Collections.Generic;

#nullable disable

namespace MVC_DangKyMonHoc.Models
{
    public partial class NamHocDkmh
    {
        public NamHocDkmh()
        {
            CongDangKies = new HashSet<CongDangKy>();
        }

        public string MaNh { get; set; }
        public string TenNh { get; set; }

        public virtual ICollection<CongDangKy> CongDangKies { get; set; }
    }
}
