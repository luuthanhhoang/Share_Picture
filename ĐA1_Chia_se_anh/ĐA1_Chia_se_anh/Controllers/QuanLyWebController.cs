using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ĐA1_Chia_se_anh.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;

namespace ĐA1_Chia_se_anh.Controllers
{
    public class QuanLyWebController : Controller
    {
        ChiaSeAnhEntities db = new ChiaSeAnhEntities();
        // GET: QuanLyWeb
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(db.Anhs.ToList().OrderBy(n => n.MaA).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult TaiKhoan()
        {
            return View(db.CTNguoiDungs.ToList());
        }
        public ActionResult BaiViet()
        {
            return View(db.BaiViets.ToList());
        }
        public ActionResult AlBum()
        {
            return View(db.AlBums.ToList());
        }
        public ActionResult NgheSi()
        {
            return View(db.NgheSis.ToList());
        }
        public ActionResult ThemMoi()
        {
            ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNS = new SelectList(db.NgheSis.ToList().OrderBy(n => n.TenNS), "MaCD", "TenNS");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(Anh anh, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNS = new SelectList(db.NgheSis.ToList().OrderBy(n => n.TenNS), "MaCD", "TenNS");
            //Kiểm tra đường dẫn ảnh
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Chọn hình ảnh";
                return View();
            }
            //Thêm vào CSDL
            if (ModelState.IsValid)
            {
                //Lưu tên của file
                var fileName = Path.GetFileName(fileUpload.FileName);
                //Lưu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/HinhAnhCĐ/AnhCĐ"), fileName);
                //Kiểm tra hình ảnh đã tồn tại chưa
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                anh.HinhAnh = fileUpload.FileName;
                db.Anhs.Add(anh);
                db.SaveChanges();
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThemND(CTNguoiDung nd)
        {
            return View();
        }
        //Chỉnh sửa hình ảnh
        [HttpGet]
        public ActionResult Chinhsua(int MaAnh)
        {
            //Lấy đối tượng ảnh theo mã
            Anh anh = db.Anhs.SingleOrDefault(n => n.MaA == MaAnh);
            if (anh == null)
            {

                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", anh.MaCD);
            ViewBag.MaNS = new SelectList(db.NgheSis.ToList().OrderBy(n => n.TenNS), "MaCD", "TenNS", anh.MaNS);
            return View(anh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(Anh anh, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.MaAnh = anh.MaA;
            ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe", anh.MaCD);
            ViewBag.MaNS = new SelectList(db.NgheSis.ToList().OrderBy(n => n.TenNS), "MaCD", "TenNS", anh.MaNS);
            return RedirectToAction("Index");
        }
    }
}