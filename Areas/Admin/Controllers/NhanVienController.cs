using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreasureL.Models;
using TreasureL.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TreasureL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhanVienController : Controller
    {
        private readonly INhanVienService _nhanVienService;

        public NhanVienController(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }
        public IActionResult Index()
        {
            return View(_nhanVienService.GetNV());
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhanVien nhanVien)
        {
            _nhanVienService.CreateNV(nhanVien);
            return RedirectToAction("Index");
        }

        // GET: Admin/SanPham/Edit/5
        [HttpGet]
        
        public async Task<IActionResult> Edit(int id)
        {
            
            return View(_nhanVienService.GetNVById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NhanVien nhanVien)
        {
            var result = _nhanVienService.UpdateNhanVien(id, nhanVien);
            if (result)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Có lỗi xảy ra khi chỉnh sửa nhân viên. Vui lòng thử lại sau");
            return View(_nhanVienService.GetNVById(id));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var result = _nhanVienService.DeleteNV(id);
            return RedirectToAction("Index");
        }
    }
}
