using TreasureL.Models;
using TreasureL.Models.Common;
using System.Collections.Generic;

namespace TreasureL.Interfaces
{
    public interface ISanPhamService
    {
        public PageResult<SanPham> GetAll(int page = 1, int? limit = 10);
        public SanPham Get(int id);

        public List<SanPham> GetRandomSanPham();
        public List<SanPham> GetListSanPhamByTheLoai(int idTheLoai);
        public List<SanPham> GetListSanPhamByTheLoai(int idTheLoai, int idSanPham, int take);
        List<SanPham> Search(string p);
        List<LoaiSp> GetAllLoaiSP(string p);
        void SaveCart(Cart cart, KhachHang khachHang);
    }
}
