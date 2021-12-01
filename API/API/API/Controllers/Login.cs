using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.XuLy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        private DangKyMonHocContext db = new DangKyMonHocContext();
        [HttpPost]
        public ActionResult PostSignIn(GiangVien a)
        {
            GiangVien b = db.GiangViens.SingleOrDefault(x => x.MaGv == a.MaGv);
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(a.Matkhau, b.Matkhau);
            if (isValidPassword == true)
            {
                if (b.Trangthai == true) return Ok();
                return BadRequest();
            }
            else
                return BadRequest();
        }

        [HttpPost("{nhanvien}")]
        public ActionResult PostSignIn(NhanVien a)
        {
            NhanVien b = db.NhanViens.SingleOrDefault(x => x.MaNv == a.MaNv);
            if (b == null)
            {
                return NotFound();
            }
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(a.Matkhau, b.Matkhau.Trim());
            if (isValidPassword == true)
            {
                if (b.Trangthai == true) return Ok(b);
                return BadRequest();
            }    
            else
                return BadRequest();

        }
        [HttpGet("{nhanvien}/{id}")]
        public ActionResult getSignIn(string id)
        {
            NhanVien b = db.NhanViens.SingleOrDefault(x => x.MaNv == id.Trim());
            return Ok(b);
        }
        [HttpPut("{nhanvien}")]
        public ActionResult PostThayDoiMK(NhanVien a)
        {
            NhanVien b = db.NhanViens.SingleOrDefault(x => x.MaNv == a.MaNv);
            if (b == null)
            {
                return NotFound();
            }
            Input_Ma i = new Input_Ma();

            b.Matkhau = i.hashPassword(a.Matkhau.Trim());
            db.SaveChanges();
            return Ok(b);

        }

        [HttpPost("{SignIn}/{SinhVien}")]
        public ActionResult PostSignIn(SinhVien a)
        {
            SinhVien b = db.SinhViens.SingleOrDefault(x => x.MaSv == a.MaSv);
            if (b == null) return BadRequest();
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(a.Matkhau, b.Matkhau.Trim());
            if (isValidPassword == true)
            {
                if (b.Trangthai == true) return Ok(b);
                return BadRequest();
            }
            else
                return BadRequest();

        }
        
        [HttpPut("{SinhVien}/{masv}")]
        public IActionResult putTTCNSinhVien(SinhVien sv)
        {
            var sinhvien = db.SinhViens.Find(sv.MaSv);
            if (sinhvien == null) return NotFound();
            sinhvien.Email = sv.Email;
            sinhvien.Sdt = sv.Sdt;
            db.SaveChanges();
            return Ok();
        }

        [HttpPost("{SinhVien}/{MatKhau}/{Resset}")]
        public IActionResult postttEditPasswordSV(EditPassword edit)
        {
            var a = db.SinhViens.Find(edit.ma);
            if (a == null) return NotFound();
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(edit.matkhaucu, a.Matkhau.Trim());
            if (isValidPassword == false)
            {
                return BadRequest("Nhập sai mật khẩu hiện tại");
            }
            
            Input_Ma input = new Input_Ma();
            a.Matkhau = input.hashPassword(edit.matkhaumoi);
            db.SaveChanges();
            return Ok();
        }
    }
}
