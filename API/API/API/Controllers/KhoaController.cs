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
	public class KhoaController : ControllerBase
	{
		private DangKyMonHocContext db = new DangKyMonHocContext();
		[HttpGet]
		public IEnumerable<Khoa> getAll()
		{
			return db.Khoas.ToList();
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<Khoa>> getKhoa(string id)
		{
			
			Khoa a = await db.Khoas.FindAsync(id);
			if (a != null)
				return Ok(a);
			else
				return NotFound();


		}
		[HttpPost]
		public async Task<IActionResult> postKhoa(Khoa khoa)
		{
			if (ModelState.IsValid)
			{
				db.Khoas.Add(khoa);
				await db.SaveChangesAsync();
				return Ok();
			}
			return BadRequest();
		}
		[HttpDelete("{id}")]
	public async Task<IActionResult>deleteKhoa(string id)
		{
			var giangvien = db.GiangViens.FirstOrDefault(x => x.MaKhoa == id);
			var nganh = db.Nganhs.FirstOrDefault(x => x.MaKhoa == id);
			if (giangvien != null || nganh != null)
				return BadRequest();
			Khoa a =  await  db.Khoas.FindAsync(id);
			if(a==null)
			{
				return NotFound();
			}
			db.Khoas.Remove(a);
			await db.SaveChangesAsync();
			return Ok();
		}
		[HttpPut("{id}")]
		public async Task<IActionResult>PutKhoa(Khoa khoa)
		{
			Khoa ob = await db.Khoas.FindAsync(khoa.MaKhoa);
			if (ob == null)
				return NotFound();
			
			ob.TenKhoa = khoa.TenKhoa;
			ob.TenVietTat = khoa.TenVietTat;
			await db.SaveChangesAsync();
			return Ok();


		}
	}
}
