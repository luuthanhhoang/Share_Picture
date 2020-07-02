using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ĐA1_Chia_se_anh.Models;

namespace ĐA1_Chia_se_anh.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        ChiaSeAnhEntities db = new ChiaSeAnhEntities();
        public ActionResult Index()
        {
            return View(db.AlBums.Where(n=>n.Moi==1).ToList());
        }
        public PartialViewResult DienDanPartial()
        {
            var lstBaiVietMoi = db.BaiViets.Take(5).ToList();
            return PartialView(lstBaiVietMoi);
        }
        public ViewResult ChiTietDienDan(int MaBV)
        {
            BaiViet ab = db.BaiViets.SingleOrDefault(n => n.MaBaiViet == MaBV);
            if (ab == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ab);
        }
        public PartialViewResult NgheSiPartial()
        {
            var lstNgheSi = db.NgheSis.Take(4).ToList();
            return PartialView(lstNgheSi);
        }
        public ViewResult XemChiTietNS(int MaNgheSi)
        {
            NgheSi ngheSi = db.NgheSis.SingleOrDefault(n => n.MaNS == MaNgheSi);
            if (ngheSi == null)
            {
                //Trả về trang nào đó báo lỗi
                Response.StatusCode = 404;
                return null;
            }
            return View(ngheSi);
        }
       
        public PartialViewResult HinhNenPartial()
        {
            var listHinhnen = db.Anhs.Take(8).ToList();
            return PartialView(listHinhnen);
        }
        public PartialViewResult AlBumView()
        {
            return PartialView(db.AlBums.Take(9).ToList());
        }
        //Xem chi tiết
        public ViewResult XemChiTiet(int MaNS)
        {
            NgheSi nghesi = db.NgheSis.SingleOrDefault(n => n.MaNS == MaNS);
            if(nghesi==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nghesi);
        }
        // GET: AllNgheSi
        public ViewResult AllNgheSi()
        {
            var lstNgheSi = db.NgheSis.ToList();
            return View(lstNgheSi);
        }
    }
}