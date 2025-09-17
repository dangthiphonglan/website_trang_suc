using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreasureL.Models;

namespace TreasureL.Areas.Admin.Interfaces
{
    public interface IKhachHangService
    {
        public KhachHang Login(LoginReq login);
        public List<KhachHang> GetKH();
        
        public KhachHang GetKHById(int id);
        public bool UpdateKhachHang(int id, KhachHang khachHang);
        public bool DeleteKH(int id);
        void CreateKH(KhachHang khachHang);
        bool Update(int id, KhachHang khachHang);
    }
}
