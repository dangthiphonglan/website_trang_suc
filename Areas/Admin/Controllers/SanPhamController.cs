using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TreasureL.Areas.Admin.Models;
using TreasureL.Models;

namespace TreasureL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanPhamController : Controller
    {
        private readonly TreasureLContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public SanPhamController(TreasureLContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;

        }

        // GET: Admin/SanPham
        public async Task<IActionResult> Index()
        {
            var TreasureLContext = _context.SanPhams.Include(s => s.MaLoaiSpNavigation);
            return View(await TreasureLContext.ToListAsync());
        }

        // GET: Admin/SanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaLoaiSpNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }



        // GET: Admin/SanPham/Create
        public IActionResult Create()
        {
            ViewBag.TenLoaiSp = new SelectList(_context.LoaiSps, "MaLoaiSp", "MaLoaiSp");
            return View();

        }

        //POST: Admin/SanPham/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPhamRequest sanPham)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(sanPham.ImageFile.FileName);
                string extension = Path.GetExtension(sanPham.ImageFile.FileName);
                string name = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                sanPham.HinhSp = "Images/" + name;
                string path = Path.Combine(wwwRootPath + "/Images/", name);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await sanPham.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TenLoaiSp"] = new SelectList(_context.LoaiSps, "MaLoaiSp", "TenLoaiSp", sanPham.MaLoaiSpNavigation.TenLoaiSp);
            return View(sanPham);
        }

        // GET: Admin/SanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            ViewData["TenLoaiSp"] = new SelectList(_context.LoaiSps, "MaLoaiSp", "TenLoaiSp", sanPham.MaLoaiSp);
            return View(sanPham);
        }

        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSp,TenSp,MaLoaiSp,DonViTinh,DonGia,HinhSp,TrangThai")] SanPham sanPham)
        {
            if (id != sanPham.MaSp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.MaSp))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoaiSp"] = new SelectList(_context.LoaiSps, "MaLoaiSp", "MaLoaiSp", sanPham.MaLoaiSp);
            return View(sanPham);
        }

        // GET: Admin/SanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .Include(s => s.MaLoaiSpNavigation)
                .FirstOrDefaultAsync(m => m.MaSp == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            _context.SanPhams.Remove(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPhams.Any(e => e.MaSp == id);
        }
        
    }
}
