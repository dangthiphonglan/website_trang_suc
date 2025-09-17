using Microsoft.AspNetCore.Mvc;
using TreasureL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreasureL.Areas.Admin.Interfaces;

namespace TreasureL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonHangController : Controller
    {

        private readonly IDonHangService _donHangService;

        public DonHangController(IDonHangService donHangService)
        {
            _donHangService = donHangService;
        }
        public IActionResult Index()
        {
            return View(_donHangService.GetDH());

        }
        //GET: Admin/SanPham/Edit/5
        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            return View(_donHangService.GetDHById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DonHang donHang)
        {
            var result = _donHangService.UpdateDonHang(id, donHang);
            if (result)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Có lỗi xảy ra khi chỉnh sửa đon hàng. Vui lòng thử lại sau");
            return View(_donHangService.GetDHById(id));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = _donHangService.DeleteDH(id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DonHang donHang)
        {
            _donHangService.CreateDH(donHang);
            return RedirectToAction("Index");
        }

    }
}
