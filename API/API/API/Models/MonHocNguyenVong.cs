using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class MonHocNguyenVong
    {
        public string maCDK { get; set; }
        public string maMH { get; set; }
        public string tenMH { get; set; }
        public int soluong { get; set; }
        public bool TrangThai { get; set; }
    }
}
