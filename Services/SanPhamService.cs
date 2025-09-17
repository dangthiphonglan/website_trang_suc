using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TreasureL.Interfaces;
using TreasureL.Models;
using TreasureL.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TreasureL.Services
{
    public class SanPhamService : ISanPhamService
    {
        private readonly TreasureLContext _context;
        public SanPhamService(TreasureLContext context)
        {
            _context = context;
        }
        // Get ID Product 
        public SanPham Get(int id)
        {
            return _context.SanPhams.Include(t => t.MaLoaiSpNavigation).Where(t => t.MaSp == id).FirstOrDefault();
        }
        // Return List All Sanpham
        public PageResult<SanPham> GetAll(int page = 1, int? limit = 12)
        {
            var pageResult = new PageResult<SanPham>()
            {
                PageIndex = page,
                PageSize = (int)limit,
                TotalRecords = _context.SanPhams.Count(),
                Items = _context.SanPhams.Skip((int)((page - 1) * limit)).Take((int)limit).ToList()
            };

            return pageResult;
        }

        public List<LoaiSp> GetAllLoaiSP(string p)
        {
            return _context.LoaiSps.ToList();
        }

        public List<SanPham> GetListSanPhamByTheLoai(int idTheLoai)
        {
            return _context.SanPhams.Where(t => t.MaLoaiSp == idTheLoai).ToList();
        }

        public List<SanPham> GetListSanPhamByTheLoai(int idTheLoai, int idSanPham,int take)
        {
            return _context.SanPhams.Where(t => t.MaLoaiSp == idTheLoai && t.MaSp != idSanPham).Take(take).ToList();
        }

        public List<SanPham> GetRandomSanPham()
        {
            var rand = new Random();
            List<SanPham> lt = new List<SanPham>();
            for (int i = 0; i < 3; i++)
            {
                lt.Add(_context.SanPhams.AsEnumerable().ElementAt(rand.Next(_context.SanPhams.Count() - 1)));
            }
            return lt;
        }

        public void SaveCart(Cart cart, KhachHang khachHang)
        {
            DonHang dh = new DonHang();

            dh.NgayLapHd = DateTime.Now;
            dh.MaKh = khachHang.MaKh;
            dh.NgayGh = DateTime.Now.AddDays(1);

            List<Ctdh> lt = new List<Ctdh>();

            foreach (var item in cart.Items)
            {
                lt.Add(new Ctdh()
                {
                    SoLuong = item.SoLuong,
                    MaSp = item.sanPham.MaSp
                });
            }

            _context.DonHangs.Add(dh);
            _context.SaveChanges();

        }

        public List<SanPham> Search(string p)
        {

            return _context.SanPhams.Where(t => t.TenSp.Contains(p)).ToList();
        }
    }
}
