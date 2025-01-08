using AutoMapper;
using ECommerceMVC.Data;
using ECommerceMVC.ViewModels;

namespace ECommerceMVC.Helpers
{
    public class AutoMapperProfile : Profile
    {
        // tu động map qua lại giữa 2 class
        public AutoMapperProfile()
        {
            CreateMap<RegisterVM, KhachHang>();
                //.ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen))
                //.ReverseMap();
            
        }
    }
}
