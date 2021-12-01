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
	public class HocKy_DkmhController : ControllerBase
	{
		private DangKyMonHocContext db = new DangKyMonHocContext();

		[HttpGet]
		public IEnumerable<HocKyDkmh> getAll()
		{
			return db.HocKyDkmhs.ToList();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<HocKyDkmh>> getHocKyDkmh(string id)
		{
			HocKyDkmh a = await db.HocKyDkmhs.FindAsync(id);
			if (a != null)
			{
				return Ok(a);
			}
			else
				return NotFound();

		}
		[HttpPost]
		public async Task<IActionResult> postHocKyDkmh(HocKyDkmh hocky)
		{
			if (ModelState.IsValid)
			{
				db.HocKyDkmhs.Add(hocky);
				await db.SaveChangesAsync();
				return Ok();
			}
			return BadRequest();
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> deleteHocKyDkmh(string id)
		{
			var kq = db.CongDangKies.SingleOrDefault(x => x.MaHk == id);
			if (kq != null)
				return BadRequest();
			HocKyDkmh a = await db.HocKyDkmhs.FindAsync(id);
			if (a == null)
				return NotFound();
			db.HocKyDkmhs.Remove(a);
			await db.SaveChangesAsync();
			return Ok();

		}
		[HttpPut]
		public async Task<IActionResult> PutHocKyDkmh(HocKyDkmh a)
		{
			HocKyDkmh hocky = await db.HocKyDkmhs.FindAsync(a.MaHk);
			if (hocky == null)
				return BadRequest();
			hocky.TenHk = a.TenHk;
			await db.SaveChangesAsync();
			return Ok();
		}
	}
}
