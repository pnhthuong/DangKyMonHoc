using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using API.Models;
using API.XuLy;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
		private DangKyMonHocContext _db = new DangKyMonHocContext();
		private readonly Input_Ma xuly = new Input_Ma();
		[HttpGet]
		public IEnumerable<NhanVien> GetDSNV()
		{
			return _db.NhanViens.ToList();
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<NhanVien>> GetNhanVien(string id)
		{
			var a = await _db.NhanViens.FindAsync(id);
			if (a == null)
				return NotFound();
			else
				return Ok(a);

		}
		

		[HttpPost]
		public async Task<ActionResult<NhanVien>> PostNhanvien(NhanVien a)
		{
			a.MaNv = a.MaChucVu.Trim() + a.MaNv.Trim();

			var checkNV=await _db.NhanViens.FindAsync(a.MaNv.Trim());
			if (checkNV != null) return NotFound();

			//Xu ly du lieu: 
			 // cau truc NV001 (nv: ma chuc vu)
			a.Matkhau = xuly.hashPassword(a.Matkhau);//BCrypt
			a.Hinhanh = a.MaNv.Trim();
			_db.NhanViens.Add(a);
			await _db.SaveChangesAsync();
			return Ok(a);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteNhanvien(string id)
		{
			var checkNV = await _db.NhanViens.FindAsync(id.Trim());
			if (checkNV == null) return NotFound();
			_db.NhanViens.Remove(checkNV);
			await _db.SaveChangesAsync();
			return Ok();
		}

		//ERROR
		[HttpPut("{id}")]
		public async Task<IActionResult> PutNhanVien(NhanVien a)
		{
			var nv = await _db.NhanViens.FindAsync(a.MaNv);
			if (nv == null)
				return NotFound();
			nv.TenNv = a.TenNv;
			nv.Diachi = a.Diachi;
			nv.Ngaysinh = a.Ngaysinh;
			nv.Sdt = a.Sdt;
			nv.Phai = a.Phai;
			nv.Cmnd = a.Cmnd;
			nv.Email = a.Email;
			nv.Trangthai = a.Trangthai;
			nv.Hinhanh = a.Hinhanh;
			//nv.Matkhau = xuly.hashPassword(a.Matkhau);
			_db.NhanViens.Update(nv);
			await _db.SaveChangesAsync();
			return Ok();

		}

		[HttpPut("{Password}/{id}")]
		public async Task<IActionResult> PutPasswordNV(NhanVien a)
		{
			var nv = await _db.NhanViens.FindAsync(a.MaNv);
			if (nv == null)
				return NotFound();
			nv.Matkhau = xuly.hashPassword(a.Matkhau);
			_db.NhanViens.Update(nv);
			await _db.SaveChangesAsync();
			return Ok();

		}

	}
}
