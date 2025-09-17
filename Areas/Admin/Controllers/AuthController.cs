using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreasureL.Extensions;
using TreasureL.Interfaces;
using TreasureL.Models;

namespace TreasureL.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly INhanVienService _nhanVienService;
        public AuthController(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        [Route("~/admin/dang-nhap")]
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.Get<NhanVien>("user-admin") != null)
                return Redirect("/admin");
            return View();
        }


        [Route("~/admin/logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Set<NhanVien>("user-admin", null);
            return Redirect("/admin/dang-nhap");
        }

        [Route("~/admin/dang-nhap")]
        [HttpPost]
        public IActionResult Login(LoginReq loginReq)
        {
            var result = _nhanVienService.Login(loginReq);
            if (result == null)
                return View();
            HttpContext.Session.Set<NhanVien>("user-admin", result);
            return Redirect("/admin");
        }
    }
}
