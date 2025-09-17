using Microsoft.AspNetCore.Mvc;
using TreasureL.Interfaces;
using TreasureL.Models.Common;
using TreasureL.Models;
using System.Linq;

namespace TreasureL.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ISanPhamService _sanPhamService;

        public SanPhamController(ISanPhamService sanPhamService)
        {
            _sanPhamService = sanPhamService;
        }

        [Route("~/san-pham/{theloai?}")]
        public IActionResult Index(int? loaiSanPham)
        {
            if (loaiSanPham.HasValue)
            {
                var sanPhams = _sanPhamService.GetListSanPhamByTheLoai(loaiSanPham.Value);
                return View(sanPhams);
            }
            else
            {
                // Nếu không có loại sản phẩm được chọn, lấy tất cả sản phẩm
                return View(_sanPhamService.GetAll());
            }
        }


        [Route("~/san-pham/{id}/_chi-tiet")]
        public IActionResult Detail(int id)
        {
            var sanpham = _sanPhamService.Get(id);

            var SanPhamTheLoai = _sanPhamService.GetListSanPhamByTheLoai((int)sanpham.MaLoaiSp, sanpham.MaSp, 3);
            ViewBag.SanPhamTheLoai = SanPhamTheLoai;
            return View(sanpham);
        }

        [HttpGet]   
        [Route("~/san-pham/tim-kiem")]
        public IActionResult Search(string p)
        {
            ViewBag.p = p;
            return View(_sanPhamService.Search(p));
        }

        [HttpGet]
        [Route("~/san-pham/loai-sp")]
        public IActionResult GetAllLoaiSp(string p)
        {
            return Ok(_sanPhamService.GetAllLoaiSP(p));
        }

        [HttpGet]
        public IActionResult Filter(int? loaiSanPham)
        {
            var products = _sanPhamService.GetListSanPhamByTheLoai(loaiSanPham.GetValueOrDefault());
            return PartialView("_ProductList", products);
        }

    }
}
