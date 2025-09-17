using TreasureL.Models;
using System.Collections.Generic;

namespace TreasureL.Interfaces
{
    public interface INhanVienService 
    {
        public NhanVien Login(LoginReq login);
        public List<NhanVien> GetNV();
        public NhanVien GetNVById(int id);
        public bool UpdateNhanVien(int id, NhanVien nhanVien);
        public bool DeleteNV(int id);
        void CreateNV(NhanVien nhanVien);
    }
}
