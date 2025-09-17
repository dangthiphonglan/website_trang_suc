using System;
using System.Collections.Generic;

#nullable disable

namespace TreasureL.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            Ctdhs = new HashSet<Ctdh>();
        }

        public int MaSp { get; set; }
        public string TenSp { get; set; }
        public int? MaLoaiSp { get; set; }
        public string DonViTinh { get; set; }
        public double? DonGia { get; set; }
        public string HinhSp { get; set; }
        public bool? TrangThai { get; set; }

        public virtual LoaiSp MaLoaiSpNavigation { get; set; }
        public virtual ICollection<Ctdh> Ctdhs { get; set; }
    }
}
