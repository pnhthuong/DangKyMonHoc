using API.Models;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.XuLy
{
    public class Input_Ma
    {
        private readonly DangKyMonHocContext db = new DangKyMonHocContext();
        //xu ly ma sinh vien
        public string maSinhVien(string masv, string malop)
        {
            var lop = db.Lops.FirstOrDefault(x => x.MaLop == malop);

            //var ctdt = db.ChuongTrinhDaoTaos.FirstOrDefault(x => x.MaCtdt == lop.MaCtdt);
            var nk = db.NienKhoas.Find(lop.MaNk);
            var ctdt = db.ChuongTrinhDaoTaos.Find(nk.MaCtdt);
            var nganh = db.Nganhs.FirstOrDefault(x => x.MaNganh == ctdt.MaNganh);
            var he = db.HeDaoTaos.FirstOrDefault(x => x.MaDt == ctdt.MaDt);

            var nienkhoa = db.NienKhoas.FirstOrDefault(x => x.MaNk == lop.MaNk);
            masv = he.MaDt.Trim() + nganh.MaKhoa.Trim() + nienkhoa.MaNk.Trim().Substring(0,2) + masv.Trim();

            return masv;
        }

        public string hashPassword(string pass)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(pass);
            return hash;
        }
    }
}
