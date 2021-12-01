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
	public class HocKy_CTDTController : Controller
	{
		private DangKyMonHocContext db = new DangKyMonHocContext();
		[HttpGet]
		public IEnumerable<HocKyCtdt> getAll()
		{
			 return db.HocKyCtdts.ToList();
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<HocKyCtdt>>getHocKyCTDT(string id)
		{
			HocKyCtdt a = await db.HocKyCtdts.FindAsync(id);
			if (a != null)
			{
				return Ok(a);
			}
			else
				return NotFound();

		}
		[HttpPost]
		public async Task<IActionResult>postHocKyCTDT(HocKyCtdt hocky)
		{
			if(ModelState.IsValid)
			{
				db.HocKyCtdts.Add(hocky);
				await db.SaveChangesAsync();
				return Ok();
			}
			return BadRequest();
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult>deleteHocKyCTDT(string id)
		{
			var kq = db.ChiTietCtdts.SingleOrDefault(x => x.MaHk == id);
				if (kq != null)
				return BadRequest();
			HocKyCtdt a = await db.HocKyCtdts.FindAsync(id);
			if (a == null)
				return NotFound();
			db.HocKyCtdts.Remove(a);
			await db.SaveChangesAsync();
			return Ok();

		}
		[HttpPut]
		public async Task<IActionResult>PutHocKyCTDT(HocKyCtdt a)
		{
			HocKyCtdt hocky = await db.HocKyCtdts.FindAsync(a.MaHk);
			if (hocky == null)
				return BadRequest();
			hocky.TenHk = a.TenHk;
			await db.SaveChangesAsync();
			return Ok();
		}
	}
}
