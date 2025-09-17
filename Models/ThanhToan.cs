using System;
using System.Collections.Generic;

#nullable disable

namespace TreasureL.Models
{
    public partial class ThanhToan
    {
        public ThanhToan()
        {
            DonHangs = new HashSet<DonHang>();
        }

        public int MaTt { get; set; }
        public string HinhThucTt { get; set; }

        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
