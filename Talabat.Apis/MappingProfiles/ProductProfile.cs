using AutoMapper;
using Talabat.Apis.Helpers;
using Talabat.Core.DTOs;
using Talabat.Core.Entities;

namespace Talabat.Apis.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductToReturnDto>().
                ForMember(p=>p.ProductBrand , options => options.MapFrom(S=>S.ProductBrand.Name)).
                ForMember(p=> p.ProductType , options => options.MapFrom(S=>S.ProductType)).
                ForMember(p=>p.PictureURL , options =>options.MapFrom<PictureUrlResolver>());
                
        }
    }
}
