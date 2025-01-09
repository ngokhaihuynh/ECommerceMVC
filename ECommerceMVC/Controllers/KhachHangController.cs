using AutoMapper;
using ECommerceMVC.Data;
using ECommerceMVC.Helpers;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceMVC.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly WebShopContext db;
        private readonly IMapper _mapper;

        public KhachHangController(WebShopContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult DangKy()
        {
            return View();
        }

        #region DangKy
        [HttpPost]
        public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // map từ RegisterVM sang KhachHang
                    var khachhang = _mapper.Map<KhachHang>(model);

                    khachhang.RandomKey = Helpers.MyUtil.GennerateRandomString(5);
                    khachhang.MatKhau = model.MatKhau.ToMd5Hash(khachhang.RandomKey);
                    khachhang.HieuLuc = true; // sẽ xử lý khi dùng mail để kích hoạt
                    khachhang.VaiTro = 0; // 0: khách hàng, 1: nhân viên, 2: admin
                    if (Hinh != null)
                    {
                        khachhang.Hinh = MyUtil.UploadHinh(Hinh, "KhachHang");
                    }
                    db.Add(khachhang);
                    db.SaveChanges();
                    //return RedirectToAction("DangNhap", "TaiKhoan");
                    return RedirectToAction("Index", "HangHoa");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View();
        }

        #endregion


        #region Dang nhap
        [HttpGet]
        public IActionResult DangNhap(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var khachhang = db.KhachHangs.SingleOrDefault(khachhang => khachhang.MaKh == model.UserName);
                if (khachhang == null)
                {
                    ModelState.AddModelError("Lỗi", "Tên đăng nhập không tồn tại");
                }
                else
                {
                    var matkhau = model.Password.ToMd5Hash(khachhang.RandomKey);
                    if (matkhau != khachhang.MatKhau)
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng");
                    }
                    else
                    {
                        if (!khachhang.HieuLuc)
                        {
                            ModelState.AddModelError("Lỗi", "Tài khoản chưa kích hoạt. Vui lòng kích hoạt tài khoản!");
                        }
                        else
                        {
                            if (khachhang.MatKhau != model.Password.ToMd5Hash
                                (khachhang.RandomKey))
                            {
                                ModelState.AddModelError("Lỗi", "Sai thông tin đăng nhập");
                            }
                            else
                            {
                                var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Email, khachhang.Email),
                                    new Claim(ClaimTypes.Name, khachhang.HoTen),
                                    new Claim("CustomerId", khachhang.MaKh),

                                    // claim cho phân quyền
                                    new Claim(ClaimTypes.Role, "Customer")
                                };

                                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                var clamPrincipal = new ClaimsPrincipal(claimIdentity);
                                await HttpContext.SignInAsync(clamPrincipal);

                                if (Url.IsLocalUrl(returnUrl))
                                {
                                    return Redirect(returnUrl);
                                }
                                else
                                {
                                    return RedirectToAction("/");
                                }
                            }
                        }

                    }
                }
            }
            return View();
        }
        #endregion

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
    }
}



