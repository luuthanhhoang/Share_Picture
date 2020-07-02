using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ĐA1_Chia_se_anh.Models;

namespace ĐA1_Chia_se_anh.Controllers
{
    public class AllNgheSiController : Controller
    {
        ChiaSeAnhEntities db = new ChiaSeAnhEntities();
        // GET: AllNgheSi
        public ViewResult AllNgheSi()
        {
            var lstNgheSi = db.NgheSis.ToList();
            return View(lstNgheSi);
        }
    }
}