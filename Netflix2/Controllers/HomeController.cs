using Netflix2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netflix2.Controllers
{
    public class HomeController : Controller
    {
        XemPhimEntities database = new XemPhimEntities();
        public ActionResult TrangChu()
        {
            using (var dbContext = new XemPhimEntities())
            {
                var items = dbContext.Phims.ToList();

                return View(items);
            }
        }

        public ActionResult TvShow()
        {
            return View();
        }

        public ActionResult Movie()
        {
            return View();
        }
        public ActionResult NewPopular()
        {
            return View();
        }
        public ActionResult MyList()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ChiTietPhim(int Id)
        {
            var Phim = database.Phims.FirstOrDefault(s => s.IdPhim == Id);
            return View(Phim);
        }
        public ActionResult XemPhim(int Id)
        {
            var Phim = database.Phims.FirstOrDefault(s => s.IdPhim == Id);
            return View(Phim);
        }
        public ActionResult TimKiem(string searching)
        {
            return View(database.Phims.Where(x => x.TenPhim.Contains(searching) || searching == null).ToList());
        }
        [HttpPost]
        public ActionResult AddComment(int idPhim, string noiDung, string tenNguoiDung)
        {
            int maKH = Convert.ToInt32(Session["MaKH"]);

            using (var dbContext = new XemPhimEntities())
            {
                var khachHang = dbContext.KhachHangs.FirstOrDefault(kh => kh.MaKH == maKH);
                var phim = dbContext.Phims.FirstOrDefault(p => p.IdPhim == idPhim);

                if (khachHang != null && phim != null)
                {
                    var binhLuan = new BinhLuan
                    {
                        MaKH = khachHang.MaKH,
                        IdPhim = phim.IdPhim,
                        NoiDung = noiDung,
                        TenNguoiDung = tenNguoiDung,
                        NgayBL = DateTime.Now
                    };

                    dbContext.BinhLuans.Add(binhLuan);
                    dbContext.SaveChanges();
                }
            }
            return RedirectToAction("ChiTietPhim", new { id = idPhim });
        }

    }
}