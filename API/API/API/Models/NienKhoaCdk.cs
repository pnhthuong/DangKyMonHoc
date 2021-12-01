using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class NienKhoaCdk
    {
        public int Id { get; set; }
        public string MaNk { get; set; }
        public string MaCdk { get; set; }

        public virtual CongDangKy MaCdkNavigation { get; set; }
        public virtual NienKhoa MaNkNavigation { get; set; }
    }
}
