using System;
using System.Collections.Generic;

#nullable disable

namespace TreasureL.Models
{
    public partial class DonHang
    {
        public DonHang()
        {
            Ctdhs = new HashSet<Ctdh>();
        }

        public int MaDh { get; set; }
        public int? MaKh { get; set; }
        public int? MaTt { get; set; }
        public int? MaNv { get; set; }
        public DateTime? NgayLapHd { get; set; }
        public DateTime? NgayGh { get; set; }

        public virtual KhachHang MaKhNavigation { get; set; }
        public virtual NhanVien MaNvNavigation { get; set; }
        public virtual ThanhToan MaTtNavigation { get; set; }
        public virtual ICollection<Ctdh> Ctdhs { get; set; }
    }
}
