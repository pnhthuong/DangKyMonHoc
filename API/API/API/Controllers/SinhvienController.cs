using API.XuLy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinhvienController : ControllerBase
    {
        private Models.DangKyMonHocContext dc = new Models.DangKyMonHocContext();
        private readonly Input_Ma xuly = new Input_Ma();
        [HttpGet]
        public IActionResult getDSSinhvien()
        {

            var list = dc.SinhViens.ToList();

            return Ok(list);
        }
        [HttpPost]
        public IActionResult postDSSinhvien(Models.SinhVien s)
        {
            if (ModelState.IsValid == false) return BadRequest();
            Models.SinhVien temp = dc.SinhViens.Find(s.MaSv);
            if (temp != null) return NotFound();

            s.MaSv = xuly.maSinhVien(s.MaSv, s.MaLop);
            s.Matkhau = xuly.hashPassword(s.Matkhau);
            s.Hinhanh = s.MaSv;
            dc.SinhViens.Add(s);
            dc.SaveChanges();
            return Ok(s);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteSinhvien(string id)
        {
            Models.SinhVien n = dc.SinhViens.Find(id);
            if (n == null) return NotFound();
            foreach (var t in dc.Lops.Where(x => x.MaLop == id))
            {
                return BadRequest();
            }
            dc.SinhViens.Remove(n);
            await dc.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> putSinhvien(Models.SinhVien s)
        {
            if (ModelState.IsValid == false) return BadRequest();
            Models.SinhVien temp = dc.SinhViens.Find(s.MaSv);
            if (temp == null) return NotFound();

                temp.TenSv = s.TenSv;
                temp.Diachi = s.Diachi;
                temp.Ngaysinh = s.Ngaysinh;
                temp.Phai = s.Phai;
                temp.Email = s.Email;
                temp.Sdt = s.Sdt;
                temp.Cnmd = s.Cnmd;
                temp.Hinhanh = s.Hinhanh;
                //temp.Matkhau = xuly.hashPassword(s.Matkhau);
                //temp.MaLop = s.MaLop;
            temp.Trangthai = s.Trangthai;

            await dc.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{Password}/{id}")]
        public IActionResult putPassword(Models.SinhVien s)
        {
            if (ModelState.IsValid == false) return BadRequest();
            Models.SinhVien temp = dc.SinhViens.Find(s.MaSv);
            if (temp == null) return NotFound();

            temp.Matkhau = xuly.hashPassword(s.Matkhau);
            dc.SaveChanges();
            return Ok();
        }
    }
}
