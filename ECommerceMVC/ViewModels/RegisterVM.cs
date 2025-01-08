using System.ComponentModel.DataAnnotations;

namespace ECommerceMVC.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Mã khách hàng không được để trống")]
        [MaxLength(20, ErrorMessage = "Mã khách hàng không được quá 20 ký tự")] 
        public string MaKh { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage ="*")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [MaxLength(50, ErrorMessage = "Họ tên không được quá 50 ký tự")]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }

        [Display(Name = "Giới tính")]
        public bool GioiTinh { get; set; } = true;

        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime? NgaySinh { get; set; }

        [MaxLength(100, ErrorMessage = "Địa chỉ không được quá 100 ký tự")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [MaxLength(20, ErrorMessage = "Điện thoại không được quá 20 ký tự")]
        [RegularExpression(@"(09|01[2|6|8|9])+([0-9]{8})\b", ErrorMessage = "Điện thoại không đúng định dạng")]
        [Display(Name = "Điện thoại")]
        public string DienThoai { get; set; }

        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Hình")]
        public string? Hinh { get; set; }
    }
}
