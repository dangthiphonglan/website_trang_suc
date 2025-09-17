using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System;
using TreasureL.Models;
using TreasureL.Interfaces;
using TreasureL.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TreasureL.Controllers
{
    public class AuthController : Controller
    {
        private readonly IKhachHangService _khachHangService;
        public AuthController(IKhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }


        [Route("~/dang-nhap")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("~/dang-nhap")]
        [HttpPost]
        public IActionResult Login(LoginReq loginReq)
        {
            if (ModelState.IsValid)
            {
                var result = _khachHangService.Login(loginReq);
                if (result == null)
                {
                    ModelState.AddModelError("","Tài khoản hoặc mật khẩu không đúng");
                    return View();
                }
                HttpContext.Session.Set<KhachHang>("user",result);
                return Redirect("/");
            }
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(KhachHang kh)
        {
            var rs = _khachHangService.SignIn(kh);
            if (rs)
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        [Route("~/logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Set<KhachHang>("user", null);
            return Redirect("/dang-nhap");
        }
    }
}
