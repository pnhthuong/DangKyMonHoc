using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private DangKyMonHocContext _db = new DangKyMonHocContext();
        //lấy danh sách môn học đã đăng ký: nếu chưa đăng ký thì trả về null, ngược lại trả về danh sách môn học 
        [HttpGet("{getCTPDK}/{masv}")]
        public IActionResult getPDKSinhVien(string masv)
        {
            //Tìm kiếm thông tin sinh viên
            var sv = _db.SinhViens.Find(masv);
            if (sv == null) return BadRequest();

            //kiểm tra lớp thuộc sinh viên
            var lop = _db.Lops.Find(sv.MaLop);
            if (lop == null) return BadRequest();

            //Kiểm tra lớp đó thuộc niên khóa nào
            var nienkhoa = _db.NienKhoas.Find(lop.MaNk);
            if (nienkhoa == null) return BadRequest();

            //Kiểm tra các niên khóa áp dung có niên khóa của sinh viên hay không?
            NienKhoaCdk nienkhoaapdung = _db.NienKhoaCdks.Where(x => x.MaNk == nienkhoa.MaNk).SingleOrDefault();
            if (nienkhoaapdung == null) return BadRequest();

            //Nếu có áp dụng thì kiểm tra xem  đó là cổng đăng ký nào
            var congdangky = _db.CongDangKies.Where(x => x.MaCdk == nienkhoaapdung.MaCdk).ToList();

            var check = congdangky.Find(x => x.Trangthai == true);
            if (check == null) return BadRequest();

            var pdk = _db.PhieuDangKies.Where(x => x.MaCdk == check.MaCdk && x.MaSv == masv).FirstOrDefault();
            if (pdk == null)
            {
                return Ok(null);
            }
            var listCTPDK = _db.ChiTietPdks.Where(x => x.MaPdk == pdk.MaPdk).ToList();
            List<MonHoc> listmonhoc = new List<MonHoc>();
            foreach(var a in listCTPDK)
            {
                var mh = _db.MonHocs.Find(a.MaMh);
                listmonhoc.Add(mh);
            }
            return Ok(listmonhoc);
        }

        [HttpPost("{phieudangky}")]
        public IActionResult postPDK(PhieuDangKy a)
        {
            var sv = _db.SinhViens.Find(a.MaSv);
            if (sv == null) return BadRequest();

            //kiểm tra lớp thuộc sinh viên
            var lop = _db.Lops.Find(sv.MaLop);
            if (lop == null) return BadRequest();

            //Kiểm tra lớp đó thuộc niên khóa nào
            var nienkhoa = _db.NienKhoas.Find(lop.MaNk);
            if (nienkhoa == null) return BadRequest();

            //Kiểm tra các niên khóa áp dung có niên khóa của sinh viên hay không?
            NienKhoaCdk nienkhoaapdung = _db.NienKhoaCdks.Where(x => x.MaNk == nienkhoa.MaNk).SingleOrDefault();
            if (nienkhoaapdung == null) return BadRequest();

            //Nếu có áp dụng thì kiểm tra xem  đó là cổng đăng ký nào
            var congdangky = _db.CongDangKies.Where(x => x.MaCdk == nienkhoaapdung.MaCdk).ToList();

            var checkc = congdangky.Find(x => x.Trangthai == true);
            if (checkc == null) return BadRequest();
            a.MaCdk = checkc.MaCdk;
            //tạo mới phiếu đăng ký
            var check = _db.PhieuDangKies.Where(x => x.MaSv == a.MaSv && x.MaCdk == a.MaCdk).FirstOrDefault();
            if (check == null)
            {
                _db.PhieuDangKies.Add(a);
                var pdkdb = _db.PhieuDangKies.Where(x => x.MaSv == a.MaSv && x.MaCdk == a.MaCdk).FirstOrDefault();
                foreach(var b in a.ChiTietPdks.ToList())
                {
                    b.MaPdk = pdkdb.MaPdk;
                    _db.ChiTietPdks.Add(b);
                }
                _db.SaveChanges();
                return Ok();
            }
            // Đã có phiếu đăng ký
            var listctpdk = _db.ChiTietPdks.Where(x => x.MaPdk == check.MaPdk).ToList();
            foreach(var b in a.ChiTietPdks.ToList())
            {
                var result = listctpdk.Find(x => x.MaMh == b.MaMh);
                if (result != null)//nếu đã có rồi thì qua
                {
                    listctpdk.Remove(result);//xóa trong danh sách tạm
                    continue;
                }
                b.MaPdk = check.MaPdk;
                _db.ChiTietPdks.Add(b);//thêm vào nếu chưa có
            }

            if (listctpdk.Count > 0)//kiểm tra xem các môn học không đăng ký nữa 
            {
                foreach(var b in listctpdk)
                {
                    _db.ChiTietPdks.Remove(b);// xóa đi 
                }
            }

            _db.SaveChanges();
            return Ok();
        }
    }
    
}
