using AutoMapper;
using tech_test_payment.Borders.Dtos.Response;
using tech_test_payment.Borders.Entities;
using tech_test_payment.Helpers;

namespace tech_test_payment.Borders.Mapper
{
    public class SaleMapper : Profile
    {
        public SaleMapper()
        {
            CreateMap<SaleEntitie, SaleResponse>()
                .ForMember(dest => dest.Status, map => map.MapFrom(src => GetStatusStringHelper.GetStringStatus(src.Status)))
                .ForMember(dest => dest.Date, map => map.MapFrom(src => src.Date.ToString("dd/MM/yyyy")));
            CreateMap<ProductEntitie, ProductResponse>();
            CreateMap<SalespersonEntitie, PersonResponse>()
                .ForMember(dest => dest.DocumentNumber, map => map.MapFrom(src => Convert.ToUInt64(src.DocumentNumber).ToString(@"000\.000\.000\-00")))
                .ForMember(dest => dest.PhoneNumber, map => map.MapFrom(src => Convert.ToUInt64(src.PhoneNumber).ToString("(##) # ####-####")));

        }

       
    }
}
