using TreasureL.Interfaces;
using TreasureL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreasureL.Services
{
    public class GioHangService : IGioHangService
    {
        private readonly TreasureLContext _context;
        public GioHangService(TreasureLContext context)
        {
            _context = context;
        }
        public List<ThanhToan> GetListThanhToan()
        {
            return _context.ThanhToans.ToList();
        }

       
        public async Task<bool> ThanhToanNow(Cart cart, ThanhToan tt)
        {
            DonHang dh = new DonHang()
            {
                MaKh = cart.MaKH,
                MaTt = tt.MaTt,
                NgayLapHd = DateTime.Now
            };

            List<Ctdh> lt = new List<Ctdh>();

            foreach (var item in cart.Items)
            {
                lt.Add(new Ctdh()
                {
                    MaDh = dh.MaDh,
                    SoLuong = item.SoLuong,
                    MaSp = item.sanPham.MaSp
                });
            }
            dh.Ctdhs = lt;

            await _context.DonHangs.AddAsync(dh);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
