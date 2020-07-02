using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ĐA1_Chia_se_anh.Models;

namespace ĐA1_Chia_se_anh.Controllers
{
    public class AllHoatDongController : Controller
    {
        ChiaSeAnhEntities db = new ChiaSeAnhEntities();
        // GET: AllHoatDong
        public ViewResult AllHD()
        {
            var lstBaiVietMoi = db.BaiViets.ToList();
            return View(lstBaiVietMoi);
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
    }
}