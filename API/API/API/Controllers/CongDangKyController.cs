using API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CongDangKyController : Controller
	{
		private DangKyMonHocContext db = new DangKyMonHocContext();
		[HttpGet]
		public  IEnumerable<CongDangKy> getAll()
		{
			return db.CongDangKies.ToList();
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<CongDangKy>> GetCDK(string id)
		{
			var a = await db.CongDangKies.FindAsync(id);
			if (a == null)
				return NotFound();
			else
				return Ok(a);
		}
		[HttpPost]
		public async Task<IActionResult> PostCongDangKy(CongDangKy cdk)
		{
			if (ModelState.IsValid == false)
				return BadRequest();
			var kq = await db.CongDangKies.FindAsync(cdk.MaCdk);
			if (kq != null)
				return BadRequest();
			db.CongDangKies.Add(cdk);
			await db.SaveChangesAsync();
			return Ok();
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCongDangky(string id)
		{
			CongDangKy kq = await db.CongDangKies.FindAsync(id);
			if (kq == null)
				return BadRequest();
			db.CongDangKies.Remove(kq);
		await db.SaveChangesAsync();
			return Ok();
		}
		[HttpPut]
		public async Task<IActionResult> PutCongDangKy(CongDangKy a)
		{
			CongDangKy kq = await db.CongDangKies.FindAsync(a.MaCdk);
			if (kq == null)
				return BadRequest();
			kq.TenCdk = a.TenCdk;
			kq.ThoigianBatDau = a.ThoigianBatDau;
			kq.ThoigianKetThuc = a.ThoigianKetThuc;
			kq.MaHk = a.MaHk;
			kq.MaNh = a.MaNh;
			kq.Trangthai = a.Trangthai;
			await db.SaveChangesAsync();
			return Ok();
		}
	}
}
