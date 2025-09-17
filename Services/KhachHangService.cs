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
    public class KhachHangService: IKhachHangService
    {
        private readonly TreasureLContext _context;
        public KhachHangService(TreasureLContext context)
        {
            _context = context;
        }

        public KhachHang Login(LoginReq login)
        {
            var user = _context.KhachHangs.Where(t => t.TenDangNhap.Equals(login.username) && t.MatKhau.Equals(ToSHA256(login.password))).FirstOrDefault();
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

        public List<KhachHang> GetKH()
        {
            return _context.KhachHangs.ToList();
        }

        public bool SignIn(KhachHang kh)
        {
            try
            {
                kh.MatKhau = ToSHA256(kh.MatKhau);
                _context.KhachHangs.Add(kh);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public KhachHang GetKHById(int id)
        {
            return _context.KhachHangs.Find(id);
        }

        public bool UpdateKhachHang(int id, KhachHang khachHang)
        {
            //try
            //{
                var kh = _context.KhachHangs.Find(khachHang.MaKh);
                if (kh == null) return false;

                kh.TenKh = khachHang.TenKh;
                kh.HoKh = khachHang.HoKh;
                kh.GioiTinh = khachHang.GioiTinh;
                kh.DienThoai = khachHang.DienThoai;
                kh.DiaChi = khachHang.DiaChi;
                kh.Email = khachHang.Email;
                _context.SaveChanges();
                return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

    }
}
