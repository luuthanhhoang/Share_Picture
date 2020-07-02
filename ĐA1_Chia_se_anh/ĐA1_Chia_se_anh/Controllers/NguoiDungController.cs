using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ĐA1_Chia_se_anh.Models;

namespace ĐA1_Chia_se_anh.Controllers
{
    public class NguoiDungController : Controller
    {
        ChiaSeAnhEntities db = new ChiaSeAnhEntities();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(CTNguoiDung nd)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ThongBao = "Đăng ký tài khoản thành công!";
                //Chèn dữ liệu vào bảng khách hàng
                db.CTNguoiDungs.Add(nd);
                //Lưu vào CSDL
                 db.SaveChanges();
            }
            else
            {
                ViewBag.ThongBao = "Thông tin đã tồn tại!";
            }
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {

            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f.Get("txtMatKhau").ToString();
            CTNguoiDung nd = db.CTNguoiDungs.SingleOrDefault(n => n.TenDangNhap == sTaiKhoan && n.MatKhau == sMatKhau);
            Admin ad = db.Admins.SingleOrDefault(n => n.TenDangNhap == sTaiKhoan && n.MatKhau == sMatKhau);
            if (nd != null)
            {
                ViewBag.ThongBao = "Chúc mừng bạn đăng nhập thành công!";
                Session["TenDangNhap"] = nd;
                return RedirectToAction("Index", "Home", nd);
            }
            else if (ad != null)
            {
                ViewBag.ThongBao = "Chúc mừng bạn đã đăng nhập thành công!";
                Session["TenDangNhap2"] = ad;
                return RedirectToAction("Index", "QuanLyWeb");
            }
            else
            {
                ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không đúng";
                return View();
            }
        }
    }
}