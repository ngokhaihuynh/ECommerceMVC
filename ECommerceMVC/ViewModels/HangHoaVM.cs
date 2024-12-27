namespace ECommerceMVC.ViewModels
{
    public class HangHoaVM
    {
        public int MaHh { get; set; }
        public String TenHH { get; set; }
        public String Hinh { get; set; }
        public double DonGia { get; set; }
        public String MoTaNgan { get; set; }
        public String TenLoai { get; set; }
    }

    public class ChiTietHangHoaVM
    {
        public int MaHh { get; set; }
        public String TenHH { get; set; }
        public String Hinh { get; set; }
        public double DonGia { get; set; }
        public String MoTaNgan { get; set; }
        public String TenLoai { get; set; }
        public String ChiTiet { get; set; }
        public int DiemDanhGia { get; set; }
        public int SoLuongTon { get; set; }
    }
}
