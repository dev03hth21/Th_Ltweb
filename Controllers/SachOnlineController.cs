using PagedList;
using SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SachOnline.Controllers
{
    public class SachOnlineController : Controller
    {
        // GET: SachOnline
        private string connection;
        private dbSachOnlineDataContext data;

        public SachOnlineController()
        {
            // Khởi tạo chuỗi kết nối
            connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SachOnline;Integrated Security=True";
            data = new dbSachOnlineDataContext(connection);
        }
        private List<SACH> LaySachMoi(int count) 
        {
            return data.SACHes.OrderByDescending(a=> a.NgayCapNhat).Take(count).ToList();
        }

        public ActionResult Index()
        {
            //lay 6 quyen sach moi
            var listSachMoi = LaySachMoi(6);

            return View(listSachMoi);
        }
        public ActionResult ChuDePartial()
        {
            var listChuDe = from cd in data.CHUDEs select cd;
            return PartialView(listChuDe);
        }
        public ActionResult NhaXuatBanPartial() 
        {
            var listNhaXuatBan = from cd in data.NHAXUATBANs select cd;
            return PartialView(listNhaXuatBan);
        }
        public ActionResult SachTheoChuDe(int iMaCD, int ? page)
        {
            ViewBag.MaCD = iMaCD;
            //tạo biến quy định số sản phẩm cho mỗi trang
            int iSize = 3;
            //tạo biến số trang
            int iPagenum = (page ?? 1 );    
            var sach = from s in data.SACHes where s.MaCD==iMaCD select s;
            return View(sach.ToPagedList(iPagenum,iSize));
        }
        public ActionResult ChiTietSach(int id)
        {
            var sach = from s in data.SACHes where s.MaSach == id select s;
            return View(sach.Single());
        }
        public ActionResult SachTheoNhaXuatBan(int id)
        {
            var sach = from s in data.SACHes where s.MaSach == id select s;
            return View(sach.Single());
        }
    }
}
