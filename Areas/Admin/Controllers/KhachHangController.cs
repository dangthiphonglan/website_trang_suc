using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreasureL.Models;
using Microsoft.EntityFrameworkCore;
using TreasureL.Areas.Admin.Interfaces;

namespace TreasureL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KhachHangController : Controller
    {
        private readonly IKhachHangService _khachHangService;

        public KhachHangController(IKhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }
        //
        public IActionResult Index()
        {
            return View(_khachHangService.GetKH());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhachHang khachHang)
        {
            _khachHangService.CreateKH(khachHang);
            return RedirectToAction("Index");
        }

        //GET: Admin/SanPham/Edit/5
        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            return View(_khachHangService.GetKHById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KhachHang khachHang)
        {
            var result = _khachHangService.Update(id, khachHang);
            if (result)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Có lỗi xảy ra khi chỉnh sửa nhân viên. Vui lòng thử lại sau");
            return View(_khachHangService.GetKHById(id));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = _khachHangService.DeleteKH(id);
            return RedirectToAction("Index");
        }
    }
}
