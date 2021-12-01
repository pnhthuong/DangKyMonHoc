using API.Models;
using API.XuLy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GiangVienController : ControllerBase
	{
		DangKyMonHocContext db = new DangKyMonHocContext();
		private readonly Input_Ma xuly = new Input_Ma();
		[HttpGet]
		public IEnumerable<GiangVien> getDSGv()
		{
			return db.GiangViens.ToList();
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<GiangVien>> getGv(string id)
		{
			GiangVien a = await db.GiangViens.FindAsync(id);
			if (a == null)
				return NotFound();
			return Ok(a);

		}

		
		[HttpPost]
		public async Task<IActionResult> postGv(GiangVien a)
		{
			if (ModelState.IsValid)
			{
				//oUser.Password = BCrypt.Net.BCrypt.HashPassword(oUser.Password);
				//Global.Users.Add(oUser);
				//return oUser;
				a.MaGv = a.MaChucVu.Trim() + a.MaGv.Trim();
				a.Matkhau = xuly.hashPassword(a.Matkhau);
				a.Hinhanh = a.MaGv.Trim();
				db.GiangViens.Add(a);
				await db.SaveChangesAsync();
				return Ok(a);

			}
			return BadRequest();
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteGiangVien(string id)
		{
			
			var Gv = await db.GiangViens.FindAsync(id);
			if (Gv == null)
				return NotFound();
			db.GiangViens.Remove(Gv);
			await db.SaveChangesAsync();
			return Ok();
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> PutGiangVien(GiangVien a)
		{
			GiangVien b = await db.GiangViens.FindAsync(a.MaGv);
			if (b == null)
				return BadRequest();
			b.TenGv = a.TenGv;
			b.Email = a.Email;
			b.MaChucVu = a.MaChucVu;
			b.Sdt = a.Sdt;
			b.Diachi = a.Diachi;
			b.Ngaysinh = a.Ngaysinh;
			b.Hocham = a.Hocham;
			b.MaKhoa = a.MaKhoa;
			b.MaChucVu = a.MaChucVu;
			b.Hinhanh = a.Hinhanh;
			await db.SaveChangesAsync();
			return Ok();
		}

		[HttpPut("{Password}/{id}")]
		public async Task<IActionResult> PutPassword(GiangVien a)
		{
			GiangVien b = await db.GiangViens.FindAsync(a.MaGv);
			if (b == null)
				return BadRequest();
			Input_Ma inp = new Input_Ma();
			b.Matkhau = inp.hashPassword(b.Matkhau);
			await db.SaveChangesAsync();
			return Ok();
		}
	}
}

