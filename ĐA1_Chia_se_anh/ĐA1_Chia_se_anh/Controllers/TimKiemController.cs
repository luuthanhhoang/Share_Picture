using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ĐA1_Chia_se_anh.Models;
using PagedList.Mvc;
using PagedList;

namespace ĐA1_Chia_se_anh.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        ChiaSeAnhEntities db = new ChiaSeAnhEntities();
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f, int ? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            List<Anh> lstKQTK = db.Anhs.Where(n => n.HinhAnh.Contains(sTuKhoa)).ToList();      
            //Phân trang 
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy hình ảnh nào";
                return View(db.Anhs.OrderBy(n => n.HinhAnh).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count + " kết quả";
            return View(lstKQTK.OrderBy(n=>n.NgayCapNhat).ToPagedList(pageNumber, pageSize));
        }
    }
}