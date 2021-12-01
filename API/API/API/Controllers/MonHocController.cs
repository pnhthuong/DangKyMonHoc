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
	public class MonHocController : ControllerBase
	{
		private DangKyMonHocContext db = new DangKyMonHocContext();
		[HttpGet]
		public IEnumerable<MonHoc> getAllDSMH()
		{
			return db.MonHocs.ToList();
		}
		[HttpGet("{id}")]
		public IActionResult getctmonhoc(string id)
		{
			MonHoc mh = db.MonHocs.Find(id.Trim());
			return Ok(mh);
		}
		[HttpPost]
		public async Task<IActionResult> PostMonHoc(MonHoc monHoc)
		{
			if (ModelState.IsValid == false)
				return BadRequest();
			MonHoc a = db.MonHocs.Find(monHoc.MaMh);
			if (a != null)
				return BadRequest();

			
			db.MonHocs.Add(monHoc);
			await db.SaveChangesAsync();
			return Ok();


		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Deletemonhoc(string id)
		{
			var checkMH = await db.MonHocs.FindAsync(id.Trim());
			if (checkMH == null) return NotFound();
			var ctdt = db.ChiTietCtdts.SingleOrDefault(x => x.MaMh == id);
			var ctpdk = db.ChiTietPdks.SingleOrDefault(x => x.MaMh == id);
			var Lopmonhoc = db.LopMonHocs.SingleOrDefault(x => x.MaMh == id);
			var monhocduocmo = db.MonHocDuocMos.SingleOrDefault(x => x.MaMh == id);
			if (ctdt != null || ctpdk != null || Lopmonhoc != null || monhocduocmo != null)
				return BadRequest();
			db.MonHocs.Remove(checkMH);
			await db.SaveChangesAsync();
			return Ok();
		}
		[HttpPut]
		public async Task<IActionResult> PutMonhoc(MonHoc monHoc)
		{
			MonHoc a = await db.MonHocs.FindAsync(monHoc.MaMh);
			if (a == null)
				return BadRequest();
			a.TenMh = monHoc.TenMh;
			a.Sotinchi = monHoc.Sotinchi;
			a.HesoHp = monHoc.HesoHp;
			a.Thuchanh = monHoc.Thuchanh;
			a.MaSh = monHoc.MaSh;
			a.MaTq = monHoc.MaTq;
			await db.SaveChangesAsync();
			return Ok();
		}

		[HttpGet("{DSMonHoc}/{DuocMo}/{id}")]
		public IActionResult getdsmonhocduocmosv(string id)
		{
			//Tìm kiếm thông tin sinh viên
			var sv = db.SinhViens.Find(id);
			if (sv == null) return BadRequest();

			//kiểm tra lớp thuộc sinh viên
			var lop = db.Lops.Find(sv.MaLop);
			if (lop == null) return BadRequest();

			//Kiểm tra lớp đó thuộc niên khóa nào
			var nienkhoa = db.NienKhoas.Find(lop.MaNk);
			if (nienkhoa == null) return BadRequest();

			//Kiểm tra các niên khóa áp dung có niên khóa của sinh viên hay không?
			List<NienKhoaCdk> nienkhoaapdung = db.NienKhoaCdks.Where(x => x.MaNk == nienkhoa.MaNk).ToList();
			if (nienkhoaapdung == null) return BadRequest();
			CongDangKy congdangky = null;
			foreach (var a in nienkhoaapdung)
			{
				var b = db.CongDangKies.Find(a.MaCdk);
				if (b.Trangthai == false)
				{
					continue;
				}
				congdangky = b;
				break;
			}
			//Nếu có áp dụng thì kiểm tra xem  đó là cổng đăng ký nào


			//var check = congdangky.Find(x => x.Trangthai == true);
			if (congdangky == null) return BadRequest();
			TimeSpan check_ngay = (TimeSpan)(congdangky.ThoigianKetThuc - DateTime.Now);
			if (check_ngay.Days < 0) return BadRequest(); 
			//lấy sanh sách môn học  thuộc cổng đang ký đó
			List<MonHocDuocMo> listmonhocduocmo = db.MonHocDuocMos.Where(x => x.MaCdk == congdangky.MaCdk && x.NkapDung == nienkhoa.MaNk).ToList();
			List<MonHocDuocMoCustom> listmonhoc = new List<MonHocDuocMoCustom>();

			foreach (var a in listmonhocduocmo)
			{
				MonHoc b = db.MonHocs.Find(a.MaMh);
				MonHocDuocMoCustom c = new MonHocDuocMoCustom
				{
					MaMh = a.MaMh,
					TenMh = b.TenMh,
					sotinchi = b.Sotinchi,
					hesohp = b.HesoHp,
					Soluong = a.Soluong,
					MaCdk = a.MaCdk,
					Trangthai = true,
					MaSH=b.MaSh
				};
				listmonhoc.Add(c);
			}

			return Ok(listmonhoc);
		}
		[HttpGet("{CTDT}/{DanhSach}/{ChiTiet}/{masv}")]
		public IActionResult getCTDTSV(string masv)
        {
			var sv = db.SinhViens.Find(masv.Trim());
			if (sv == null) return BadRequest();

			//kiểm tra lớp thuộc sinh viên
			var lop = db.Lops.Find(sv.MaLop);
			if (lop == null) return BadRequest();

			//Kiểm tra lớp đó thuộc niên khóa nào
			var nienkhoa = db.NienKhoas.Find(lop.MaNk);
			if (nienkhoa == null) return BadRequest();

			var ctdt = db.ChuongTrinhDaoTaos.Find(nienkhoa.MaCtdt);
			if (ctdt == null) return BadRequest();

			List<ChiTietCtdt> listctdt = db.ChiTietCtdts.Where(x => x.MaCtdt == ctdt.MaCtdt).ToList();
			if (listctdt == null) return BadRequest();
			List<CT_CTDT_SV> list = new List<CT_CTDT_SV>();
			foreach(var a in listctdt)
            {
				MonHoc b = db.MonHocs.Find(a.MaMh);
				CT_CTDT_SV ct = new CT_CTDT_SV
				{
					MaMh = a.MaMh,
					TenMh = b.TenMh,
					HesoHp = b.HesoHp,
					Sotinchi = b.Sotinchi,
					Thuchanh = b.Thuchanh,
					MaHk = a.MaHk
				};
				list.Add(ct);
            }
			list = list.OrderBy(p => p.MaHk).ToList();
			return Ok(list);
		}
		[HttpGet("monhoctest")]
		public IEnumerable<MonHoc> getMonhoc()
		{
			return db.MonHocs.ToList();
		}
    }
}
