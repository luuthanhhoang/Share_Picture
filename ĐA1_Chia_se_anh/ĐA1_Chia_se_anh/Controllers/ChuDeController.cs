using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ĐA1_Chia_se_anh.Models;
using PagedList;
using PagedList.Mvc;

namespace ĐA1_Chia_se_anh.Controllers
{
    public class ChuDeController : Controller
    {
        // GET: ChuDe
        ChiaSeAnhEntities db = new ChiaSeAnhEntities();
        public ActionResult ChuDePartial()
        {
            return PartialView(db.ChuDes.Take(5).ToList());
        }
        public ViewResult DanhMucChuDe()
        {
            return View(db.ChuDes.ToList());
        }
        //Ảnh theo chủ đề
        public ViewResult AnhTheoChuDe(int MaChuDe, int? page)
        {
            //Kiểm tra xem chủ đề tồn tại hay không
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaCD == MaChuDe);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Tạo biến số ảnh trên trang
            int pageSize = 8;
            //Tạo biến số trên trang
            int pageNumber = (page ?? 1);
            List<Anh> lstanh = db.Anhs.Where(n => n.MaCD == MaChuDe).OrderBy(n => n.NgayCapNhat).ToList();
            if(lstanh.Count==0)
            {
                ViewBag.Anh = "Không có ảnh nào thuộc chủ đề này";
            }
            return View(lstanh.ToPagedList(pageNumber,pageSize));
        }    
    }
}