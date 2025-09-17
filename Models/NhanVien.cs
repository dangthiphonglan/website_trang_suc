using System;
using System.Collections.Generic;

#nullable disable

namespace TreasureL.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            DonHangs = new HashSet<DonHang>();
        }

        public int MaNv { get; set; }
        public string TenNv { get; set; }
        public string HoNv { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public int? CaLamViec { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int? IsAdmin { get; set; }//1 là admin - 0 là nhan vien

        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
