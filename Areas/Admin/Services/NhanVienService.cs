using TreasureL.Interfaces;
using TreasureL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace TreasureL.Services
{
    public class NhanVienService : INhanVienService
    {
        private readonly TreasureLContext _context;
        public NhanVienService(TreasureLContext context)
        {
            _context = context;
        }

        public NhanVien Login(LoginReq login)
        {
            var user = _context.NhanViens.Where(t => t.TenDangNhap.Equals(login.username) && t.MatKhau.Equals(ToSHA256(login.password))).FirstOrDefault();
            if(user != null)
            {
                return user;
            }
            return null;
        }

        public string ToSHA256(string src)
        {
            SHA256 crypt = SHA256.Create();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(src));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
        public List<NhanVien> GetNV()
        {
            return _context.NhanViens.ToList();
        }

        public NhanVien GetNVById(int id)
        {
            return _context.NhanViens.Find(id);
        }

        public bool UpdateNhanVien(int id, NhanVien nhanVien)
        {
            try
            {
                var nv = _context.NhanViens.Find(id);
                if (nv == null) return false;
                nv.HoNv = nhanVien.HoNv;
                nv.TenNv = nhanVien.TenNv;
                nv.DiaChi = nhanVien.DiaChi;
                nv.DienThoai = nhanVien.DienThoai;
                nv.Email = nhanVien.Email;
                nv.CaLamViec = nhanVien.CaLamViec;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteNV(int id)
        {
            try
            {
                var nv = _context.NhanViens.Find(id);
                _context.NhanViens.Remove(nv);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CreateNV(NhanVien nhanVien)
        {
            nhanVien.MatKhau = ToSHA256("123");
            _context.NhanViens.Add(nhanVien);
            _context.SaveChanges();
        }
    }
}
