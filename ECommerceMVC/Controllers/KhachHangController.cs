using AutoMapper;
using ECommerceMVC.Data;
using ECommerceMVC.Helpers;
using ECommerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
    }
}
