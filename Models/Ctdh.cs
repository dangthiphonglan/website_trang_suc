using System;
using System.Collections.Generic;

#nullable disable

namespace TreasureL.Models
{
    public partial class Ctdh
    {
        public int MaCtdh { get; set; }
        public int? MaDh { get; set; }
        public int? SoLuong { get; set; }
        public int? MaSp { get; set; }

        public virtual DonHang MaDhNavigation { get; set; }
        public virtual SanPham MaSpNavigation { get; set; }
    }
}
