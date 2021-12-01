using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DangKyMHController : ControllerBase
	{
		private DangKyMonHocContext db = new DangKyMonHocContext();
		[HttpPost]
		public IActionResult PostPhieuDangKy(PhieuDangKy a)
		{
			var sv = db.SinhViens.Find(a.MaSv);
			if (sv == null) return BadRequest();

			//kiểm tra lớp thuộc sinh viên
			var lop = db.Lops.Find(sv.MaLop);
			if (lop == null) return BadRequest();

			//Kiểm tra lớp đó thuộc niên khóa nào
			var nienkhoa = db.NienKhoas.Find(lop.MaNk);
			if (nienkhoa == null) return BadRequest();

			//Kiểm tra các niên khóa áp dung có niên khóa của sinh viên hay không?
			NienKhoaCdk nienkhoaapdung = db.NienKhoaCdks.Where(x => x.MaNk == nienkhoa.MaNk).SingleOrDefault();
			if (nienkhoaapdung == null) return BadRequest();

			//Nếu có áp dụng thì kiểm tra xem  đó là cổng đăng ký nào
			var congdangky = db.CongDangKies.Where(x => x.MaCdk == nienkhoaapdung.MaCdk).ToList();
			// nếu có 0 hoặc > 2 thì không hợp lệ do mỗi lần chỉ có 1 cổng đăng ký 
			if (congdangky.Count == 0 || congdangky.Count > 2) return BadRequest();
			if (congdangky[0].Trangthai == false) return BadRequest();
			a.MaCdk = congdangky[0].MaCdk;
			db.PhieuDangKies.Add(a);
			db.SaveChanges();
			return Ok();
		}
		
		[HttpGet("{TimKiem}/{id}")]
		public IActionResult getmonhoc(string id)
        {
			MonHoc mh = db.MonHocs.Find(id);
			if (mh == null) return NotFound();
			MonHocDuocMoCustom mhdm = new MonHocDuocMoCustom();
			mhdm.MaMh = mh.MaMh;
			mhdm.TenMh = mh.TenMh;
			mhdm.hesohp = mh.HesoHp;
			mhdm.sotinchi = mh.Sotinchi;
			mhdm.Trangthai = false;
			return Ok(mhdm);
        }

		[HttpGet("{TimKiem}/{MonHocDuocMo}/{id}/{cdk}/{masv}")]
		public IActionResult getmonhocduocmo(string id,string cdk, string masv)
		{
			//lay danh sach mon hoc duoc mo
			var congDangKy = db.CongDangKies.Find(cdk);
			List<MonHocDuocMo> ListMHDM = db.MonHocDuocMos.Where(x => x.MaCdk == congDangKy.MaCdk).ToList();
			//lay danh chuong trinh dao tao
			var sinhvien = db.SinhViens.Find(masv);
			if (sinhvien == null) return NotFound();
			var lop = db.Lops.Find(sinhvien.MaLop);
			var nienkhoa = db.NienKhoas.Find(lop.MaNk);
			var CTDT = db.ChuongTrinhDaoTaos.Find(nienkhoa.MaCtdt);
			List<ChiTietCtdt> CTCTDT = db.ChiTietCtdts.Where(x => x.MaCtdt == CTDT.MaCtdt).ToList();

			//kiem tra mon hoc
			var monhoc = ListMHDM.Find(x => x.MaMh.Trim() == id.Trim());
			if (monhoc == null) return NotFound("Không Tìm Thấy Môn Học");
			var mh = CTCTDT.Find(x => x.MaMh == monhoc.MaMh);
			if (mh == null) return BadRequest("Môn Học Không Có Trong Chương Trình Đào Tạo Của Bạn");

            MonHoc mh1 = db.MonHocs.Find(monhoc.MaMh);
			MonHocDuocMoCustom mhdm = new MonHocDuocMoCustom();
			mhdm.MaMh = mh1.MaMh;
			mhdm.TenMh = mh1.TenMh;
			mhdm.MaCdk = cdk;
			mhdm.hesohp = mh1.HesoHp;
			mhdm.sotinchi = mh1.Sotinchi;
			mhdm.Soluong = monhoc.Soluong;
			mhdm.Trangthai = true;
			return Ok(mhdm);
		}
	}
}
