namespace ECommerceMVC.ViewModels
{
    public class CartItem
    {
        public int MaHh { get; set; }
        public String Hinh { get; set; }
        public String TenHH { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien => SoLuong * DonGia;
    }
}
