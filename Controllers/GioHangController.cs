using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreasureL.Extensions;
using TreasureL.Interfaces;
using TreasureL.Models;
using TreasureL.Services;
using System.Threading.Tasks;

namespace TreasureL.Controllers
{
    public class GioHangController : Controller
    {
        private readonly ISanPhamService _phamService;
        private readonly IGioHangService _gioHangService;
        public GioHangController(ISanPhamService phamService, IGioHangService gioHangService)
        {
            _gioHangService = gioHangService;
            _phamService = phamService;
        }
        [Route("~/gio-hang")]
        public IActionResult Index()
        {
            Cart cart = null;
            if (HttpContext.Session.Get<Cart>("cart") != null)
            {
                cart = HttpContext.Session.Get<Cart>("cart");
            }
            ViewBag.Cart = cart;
            return View();
        }

        [Route("~/thanh-toan")]
        [HttpGet]
        public IActionResult ThanhToan()
        {
            KhachHang kh = null;
            if (HttpContext.Session.Get<KhachHang>("user") == null)
            {
                return Redirect("/dang-nhap");
            }
            else
            {
                kh = HttpContext.Session.Get<KhachHang>("user");
            }
            Cart cart = null;
            if (HttpContext.Session.Get<Cart>("cart") != null)
            {
                cart = HttpContext.Session.Get<Cart>("cart");
                _phamService.SaveCart(cart, kh);
            }
            else
            {
                return RedirectToAction("Index");
            }
            ViewBag.ListThanhToan = _gioHangService.GetListThanhToan();
            return View(cart);
        }

        [Route("~/thanh-toan")]
        [HttpPost]
        public async Task<IActionResult> ThanhToan(ThanhToan tt)
        {
            Cart cart = HttpContext.Session.Get<Cart>("cart");
            KhachHang kh = HttpContext.Session.Get<KhachHang>("user");
            cart.MaKH = kh.MaKh;
            var result = await _gioHangService.ThanhToanNow(cart, tt);
            if (result)
            {
                HttpContext.Session.Set<Cart>("cart", null);
                return Redirect("/");
            }
            return View();
        }


        [Route("~/gio-hang/add")]
        [HttpPost]
        public IActionResult AddCart([FromBody]AddCart addCart)
        {
            Cart cart;
            if (HttpContext.Session.Get<Cart>("cart") != null)
            {
                cart = HttpContext.Session.Get<Cart>("cart");

                bool tem = false;
                foreach (CartItem cItem in cart.Items)
                {
                    if (cItem.sanPham.MaSp == addCart.MaSp)
                    {
                        cItem.SoLuong = cItem.SoLuong + addCart.SoLuong;
                        tem = true;
                        break;
                    }
                }
                if (tem)
                {
                    HttpContext.Session.Set<Cart>("cart", cart);
                    return Ok(cart);
                }
            }
            else
            {
                cart = new Cart();
            }

            SanPham sp = _phamService.Get((int)addCart.MaSp);

            CartItem item = new CartItem()
            {
                sanPham = sp,
                SoLuong = addCart.SoLuong
            };

            cart.Items.Add(item);

            HttpContext.Session.Set<Cart>("cart", cart);

            return Ok(cart);
        }

        [Route("~/gio-hang/update")]
        [HttpPost]
        public IActionResult UpdateItem([FromBody] AddCart addCart)
        {
            Cart cart = null;
            if (HttpContext.Session.Get<Cart>("cart") != null)
            {
                cart = HttpContext.Session.Get<Cart>("cart");
                foreach(CartItem item in cart.Items)
                {
                    if(addCart.SoLuong == 0 && item.sanPham.MaSp == addCart.MaSp)
                    {
                        cart.Items.Remove(item);
                        break;
                    }
                    if(item.sanPham.MaSp == addCart.MaSp)
                    {
                        item.SoLuong = addCart.SoLuong;
                    }
                }
                HttpContext.Session.Set<Cart>("cart", cart);
            }
            return Ok(cart);
        }

        [Route("~/gio-hang/delete/{id}")]
        [HttpDelete]
        public IActionResult DeleteItem(int id)
        {
            Cart cart = null;
            if (HttpContext.Session.Get<Cart>("cart") != null)
            {
                cart = HttpContext.Session.Get<Cart>("cart");
                foreach (CartItem item in cart.Items)
                {
                    if(id == item.sanPham.MaSp)
                    {
                        cart.Items.Remove(item);
                        break;
                    }
                }
                HttpContext.Session.Set<Cart>("cart", cart);
            }
            return Ok(cart);
        }
    }
}
