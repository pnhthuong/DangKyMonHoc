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
    public class Khoikienthuc : ControllerBase
    {
		private DangKyMonHocContext db = new DangKyMonHocContext();
		[HttpGet]
		public IEnumerable<KhoiKienThuc> getAll()
		{
			return db.KhoiKienThucs.ToList();
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<KhoiKienThuc>> getKhoiKienThuc(string id)
		{
			KhoiKienThuc a = await db.KhoiKienThucs.FindAsync(id);
			if (a != null)
			{
				return Ok(a);
			}
			else
				return NotFound();

		}
		[HttpPost]
		public async Task<IActionResult> postKhoiKienThuc(KhoiKienThuc hocky)
		{
			if (ModelState.IsValid)
			{
				
				db.KhoiKienThucs.Add(hocky);
				await db.SaveChangesAsync();
				return Ok();
			}
			return BadRequest();
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> deleteKhoiKienThuc(string id)
		{
			var kq = db.MonHocs.SingleOrDefault(x => x.MaKhoi == id);
			if (kq != null)
				return BadRequest();
			KhoiKienThuc a = await db.KhoiKienThucs.FindAsync(id);
			if (a == null)
				return NotFound();
			db.KhoiKienThucs.Remove(a);
			await db.SaveChangesAsync();
			return Ok();

		}
		[HttpPut]
		public async Task<IActionResult> PutKhoiKienThuc(KhoiKienThuc a)
		{
			KhoiKienThuc kkt = await db.KhoiKienThucs.FindAsync(a.MaKhoi);
			if (kkt == null)
				return BadRequest();
			kkt.TenKhoi = a.TenKhoi;
			kkt.TenChuyenMon = a.TenChuyenMon;
			kkt.Batbuoc = a.Batbuoc;
			await db.SaveChangesAsync();
			return Ok();
		}
	}
}
